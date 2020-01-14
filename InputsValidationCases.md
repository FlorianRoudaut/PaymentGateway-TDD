#Inputs Validation Cases

This document describes all the validation cases on incoming payment requests. 

### Merchant Name 
Should not be empty

### Card Number
Should not be empty
Can contain empty spaces
Should have 16 digits

### CVV
Should not be empty
Should be 3 or 4 digits

### Expiry Month
Should not be empty

### Expiry Year
Should not be empty
The date of the expiry month and year should be more than the current date

### Card Holder Name
Should not be empty

### Amount
Should be strictly positive

### Currency
Should not be empty
Should be in the list of available currencies defined in the database
