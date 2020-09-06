# UCLA Health Round 3 Interview Assignment

## Intro

This application reads a file following this format and displays
formatted information about the data, covered in [Scenarios](#Scenarios) below.

```
Example:
01232020Jamie
BEVGTTKYGDGJHGTFBNGDVZJGDIPXVS
CANFDNSKAVOUXSCGSYBHQYHNMDQOBL
FRZNQQNPSESCHIMIXOUHNAWLXRZEPT
BEVGZYUFGNIHDCZIPWLZJLPDSGNEAH

The first line includes the date (MMDDYYYY) as well as the customer information. Each line following that is an item barcode, in no particular order.

The barcode can be broken down into these components:
[Product Type][Subtype][Unique ID]	
[BEVG][TTKYGD][GJHGTFBNGDVZJGDIPXVS]
The product type is always 4 letters long, the subtype is 6 letters long, and the unique id is 20 letters long.
```

## Usage
For ease of use, the application accepts 0 or 1 parameters:
* If a parameter is not passed, the application will load an example file
* If a parameter is passed, it must be the path to the purchase file so the application will load it

# Scenarios

## Question 1

The application starts with outputting the following.

```
a)	Name of the customer
b)	Formatted date of purchase
c)	Total number of items they purchased

```

Here is an example of the expected output.

```
Question 1 solution:

a) Customer: Jamie
b) Date: 1/23/2020 12:00:00 AM
c) Total Items Purchased: 4
```

## Question 2
### Part 1

Following, the application then outputs the following.

```
a)	For every existing product type, the number and list of unique IDs of items purchased by the customer
b)	The most common product type
```

Here is an example of the expected output.

```
Question 2 part 1 solution:

a) The number of unique items purchased: 4
    The unique IDs that were purchased:
    GJHGTFBNGDVZJGDIPXVS
    OUXSCGSYBHQYHNMDQOBL
    SCHIMIXOUHNAWLXRZEPT
    IHDCZIPWLZJLPDSGNEAH
b) The most common product type purchased: BEVG
```

### Part 2

The application then pauses and accepts user input.

The expected input is a 4-character Product Type code, e.g., "BEVG," "MISC," etc.

The application will accept one character at a time, showing suggestions for possible Product Types as the user types.

When the 4th character is entered, the application will display the following Product Subtype information for the best match of the user's Product Type input.

```
a)	Subtypes for that product type
```

If there are no matches, the application will let the user know.

Here is an example of the expected output when the user types "BEVG"

```
Question 2 part 2 solution:

Input a 4-character Product Type to list Subtypes in this Purchase.
Press Enter to stop searching.
 - Possible matches:
        BEVG
        BAKE
        DREG
        CANF
        CNSB
        SNCN
        FRZN
        GRPA
        MISC
        MTSF
        FRVG
 - Possible matches:
        BEVG
        BAKE
 - Possible matches:
        BEVG
 - Possible matches:
        BEVG
 > BEVG

Searching for best match 'BEVG'

a) Subtypes for product type BEVG:
    TTKYGD
    ZYUFGN

Input a 4-character Product Type to list Subtypes in this Purchase.
Press Enter to stop searching.
 >
```

Either way, [Part 2](#Part-2) will repeat forever until the user presses Enter/Return.

Once this occurs, the application prompts the user to press Enter/Return to exit.

The application is finished after this.

## Question 3

The application was built with the Question 3 problem statement in mind.

```
The first line of each text file remained the same, but for some of the barcodes, a random letter was changed somewhere in product type. For example:

05242020James
FRZNQQNPSESCHIMIXOUHNAWLXRZEPT
BENGTTKYGDGJHGTFBNGDVZJGDIPXVS
CDNFDNSKAVOUXSCGSYBHQYHNMDQOBL
```

When the application first loads file data, it will ensure correct Product Type keys are parsed. It will attempt to find the intended Product Type key with the use of a BK Tree.

When a match is found, or if the Product Type key is already correct, this Product Type key is imported.

This same functionality is used for Question 2 Part 2, when the user types a Product Key.

# Technology

For technical documentation, see [TECHNOLOGY.md](docs/TECHNOLOGY.md).

# Theory

For discussion on the theory behind this application's operation, see [THEORY.md](docs/THEORY.md).

# Further Documentation

Additional documentation and UML diagrams can be found in the [docs](docs/) directory, as well as throughout the code in the form of [XMLDoc](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/) comments.