const express = require('express');
const bcrypt = require('bcrypt');
const fs = require('fs');
const router = express.Router();
const filePath = 'users.txt'; // Path to your text file

// Register Route
router.post('/register', async (req, res) => {
  const { username, password, userType } = req.body;

  // Check if the user already exists in the file
  const users = fs.readFileSync(filePath, 'utf-8').split('\n');
  if (users.some(user => user.split(':')[0] === username)) {
    return res.status(400).json({ message: 'User already exists' });
  }

  // Hash the password
  const hashedPassword = await bcrypt.hash(password, 10);

  // Append new user to the text file
  fs.appendFileSync(filePath, `${username}:${hashedPassword}\n`);

  res.status(201).json({ message: 'User registered successfully' });
});

// Login Route
router.post('/login', async (req, res) => {
  const { username, password } = req.body;

  // Read the users from the text file
  const users = fs.readFileSync(filePath, 'utf-8').split('\n');
  const user = users.find(u => u.split(':')[0] === username);

  if (!user) return res.status(400).send('User not found');

  const storedPassword = user.split(':')[1];
  const isMatch = await bcrypt.compare(password, storedPassword);
  if (!isMatch) return res.status(400).send('Invalid credentials');

  // Successfully authenticated, generate token or response
  res.json({ message: 'Login successful' });
});

module.exports = router;
