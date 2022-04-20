# Dotnet Test CLI

## Abstract

This mini package helps run `xunit` tests for a dotnet application by wrapping `dotnet test...` commands in a quasi-UI command line application.

To achieve that, it uses the [curses](https://docs.python.org/3/howto/curses.html) python default module.

## Use cases

- Run tests for an entire solution
- Run tests for one project
- Run tests for multiple projects
- Run all tests for one class
- Run all tests for multiple classes
- Run a single test of a class
- Run some tests of a class
- Run all tests of a collection
- Run some tests of a collection 

## Dotnet test options/flags

#### List all tests

- `dotnet test -t` 
- `dotnet test --list-tests`


## Usage
