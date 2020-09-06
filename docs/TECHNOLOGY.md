# Technology
This file contains information on the technology used in this application, as well as design and architecture information.

This application is a .NET Core Console application written in C#.

# Structure
The domain code is organized per [Domain-Driven Design](https://en.wikipedia.org/wiki/Domain-driven_design) (DDD) principles and guidelines.

Business logic is shared between the DDD classes and a set of classes I've named `ConsoleWriter`s.

## Domain
The domain of the application is broken down as follows.

### Aggregates
An Aggregate called `PurchaseAggregate` is responsible for processing questions about a set of Purchases (a `PurchaseEntity`) in a file. For example, this Aggregate can return the most common Product purchases in a file.

### Entities
An Entity called `PurchaseEntity` contains information on the Purchase, representing the entire Purchase file.

This Entity holds the Purchase date, Customer name, and the three parts of a Product entry in a file.

This Entity is able to construct itself from a file. It parses this file and sets the appropriate properties. No instance methods exist on this Entity.

A Purchase is considered an Entity because every Purchase is unique regardless of the data contained within that Purchase. For example, however unlikely it may be, two Customers with the same name may buy the exact same Products at the same time as another Customer.

### Values
A Value type exists for each of the following:
* Barcode (`BarcodeValue`)
* Customer (`CustomerValue`)
* Product ID (`ProductIdValue`)
* Product Type (`ProductTypeValue`)
* Product Subtype (`ProductSubtypeValue`)
* Timestamp (`TimestampValue`)

These are considered Value types because, in relation to a Purchase, each Value is equivalent based on their properties. For example, a Product ID of "BEVG" represents a Product ID of "BEVG" regardless of how many "BEVG" entries there are for a Purchase.

Review [classStructure.puml](classStructure.puml) to understand the relations between these types.

With the exception of `BarcodeValue`, these Value types extend `ValueBase<T>`. This is done to manage the code to determine equality between instances of Value types.

Each Value type overrides the default operators for `==` and `!=` in order to equate their values _by value_ rather than each instance _by reference_.

Each Value type implements implicit cast methods between `string` (except for `TimestampValue` whose `T` type is `DateTime`) types as well, simply for ease of use in treating these value types as strings.

## Business logic
The business logic for this application is primarily contained within the Main Program.cs, the `PurchaseAggregate`, and the `PurchaseEntity` classes. The code is documented and should be reviewed for further clarification.

Some of the business logic is non-Domain and is thus managed outside the DDD guidelines. In this case, the output of information about purchases in handled in classes that I've named `ConsoleWriters`.

These classes work using the [Visitor Pattern](https://en.wikipedia.org/wiki/Visitor_pattern).

In this case, the Visitor visits a writer that handles unique output for each of the `PurchaseAggregate` and `PurchaseEntity` types. The output of these Dispatchers is returned and written to the console by the Visitor.

### Searching
In order to handle corrupted Purchase Type data, I have implemented a BK Tree to discover the closest matching Product Type when inputting data, or when outputting Product Subtypes for a Product Type.

I made use of several references. These are listed in code, and here:

* https://en.wikipedia.org/wiki/Levenshtein_distance
* https://www.csharpstar.com/csharp-string-distance-algorithm
* https://www.geeksforgeeks.org/bk-tree-introduction-implementation/
* http://blog.notdot.net/2007/4/Damn-Cool-Algorithms-Part-1-BK-Trees
* https://nullwords.wordpress.com/2013/03/13/the-bk-tree-a-data-structure-for-spell-checking/

## Other Considerations
As much as possible, this code was written to be easily unit testable. As such, I have also made use of the [Wrapper Pattern](https://en.wikipedia.org/wiki/Adapter_pattern) to avoid using the static `Console` class directly.

# Unit Tests
The application is thoroughly unit tested. The tests run on the xUnit testing framework for .NET and C#.