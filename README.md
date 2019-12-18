# String Calculator

## Summary
This is a calculator that takes in a single formatted string and performs and add operation.

## Currently Supports:
1. Comma-separated numbers 
    * e.g. `1,2,3,4,5` will return `15`
1. Newlines
    * e.g. `1\n2` will return `3`; `1,2\n3` will return `6`
1. Single Delimiter using format: `//{delimiter}\n{numbers}`;
    * e.g. `//#\n2#5` will return `7`
    * NOTE: `\n` is newline
1. Single Delimiter of any length using format: `//[{delimiter}]\n{numbers}`
    * e.g. `//[***]\n11***22***3` will return `66`;
    * NOTE: `\n` is newline
1. Multiple Delimiters of any length using format: `//[{delimiter1}][{delimiter2}]...\n{numbers}`
    * e.g. `//[*][!!][r9r]\n11r9r22*hh*33!!44` will return `110`

## Constraints
1. Missing or Invalid Numbers will be converted to 0
    * Numbers greater than 1000 are Invalid Numbers
1. Negative numbers will throw an exception.