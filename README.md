# VPL course coursework - handwritten digit classifier

## Main development  stack

    - ASP.NET core 5.0
    - Blazor
    - ML.NET

## Description

    Authors: Kristofers Volkovs & Arina Solovjova
    Project type: Final Course Project for Visual Programming Languages 2020
    Project name: Written number classifier
    E-mail: krystoart@gmail.com & arinacka2@gmail.com

## Report
### Needed dependencies (tested on ubuntu 20.04)

- texlive-bibtex-extra/focal,focal,now 2019.202000218-1
- texlive-extra-utils/focal,focal,now 2019.202000218-1
- texlive-lang-all/focal,focal,now 2019.20200218-1
- texlive-latex-extra/focal,focal,now 2019.202000218-1
- texlive-xetex/focal,focal,now 2019.20200218-1
- Pyhon2 `minted` package
- Times New Roman fonts
- biber

### Compilation

These commands need to be run in order:

```sh
xelatex -synctex=1 -interaction=nonstopmode -file-line-error --shell-escape main.tex
```
```sh
biber main
```
```sh
xelatex -synctex=1 -interaction=nonstopmode -file-line-error --shell-escape main.tex
```

