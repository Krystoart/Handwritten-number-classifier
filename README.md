# VPL course coursework - handwritten digit classifier

## Development stack

    - ASP.NET core 5.0
    - Blazor
    - ML.NET

## Description

    Authors: Kristofers Volkovs & Arina Solovjova
    Project type: Final Course Project for Visual Programming Languages 2020
    Project name: Written number classifier
    E-mail: krystoart@gmail.com & arinacka2@gmail.com
## Setup

Prerequisites:

- .NET toolchain - [link](https://dotnet.microsoft.com/learn/aspnet/blazor-tutorial/intro)

To start project:

    dotnet watch run

## Route mappings

By default project is exposed to localhost:5000

## Problems

If you are working on Ubuntu then you might run into all kinds of problems.
Here are some solutions:

Re adding used packages:
```sh
dotnet add package Microsoft.ML
```
```sh
dotnet add package System.Drawing.Common --version 5.0.0
```

Adding dependencies used in the packages above:
```sh
sudo apt install libgdiplus
```
```sh
sudo apt install libc6-dev
```
