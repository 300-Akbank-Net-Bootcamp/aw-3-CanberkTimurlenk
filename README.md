# Additions

## Screaming Architecture

Foldering was mainly structured according to Screaming Architecture, as described by Robert C. Martin.

https://blog.cleancoder.com/uncle-bob/2011/09/30/Screaming-Architecture.html

The core concept of Screaming Architecture is that the project's design must accurately reflect its functionality.

Foldering Scructure
<br>
<br>
<img src="https://github.com/300-Akbank-Net-Bootcamp/aw-3-CanberkTimurlenk/assets/18058846/e8157127-cae3-4243-b78b-58728e7b6a80" alt="Resim" width="300" height="350">

## Constants

Constant values such as messages and strings are placed under the Constants folder for each related feature to centralize these concerns and prevent the 'magic string' phenomenon.

## Additional Business Rules 

Additional business rules have been added for features such as Eft & Account Transactions, and also for Contacts and Addresses.

## Additional Validations

Additional validations have been implemented using FluentValidation.

<br>
<hr>
<br>

# Getting Started

Ensure that you have the .NET SDK installed on your computer.

1. Clone the repository:

```bash
git clone https://github.com/300-Akbank-Net-Bootcamp/aw-2-CanberkTimurlenk.git
```
2. Change connection string with yourself

The connection string is located in ``/Vb.Api/appsettings.json`` <br>
It must be written correctly to apply migrations successfully.

3. Navigate to Data Project
```bash
cd /Vb.Data
```
4. Apply migrations (SQL Server Required!)
```bash
dotnet ef database update --project "./Vb.Data" --startup-project "./Vb.Api"
```
5. Navigate to startup project
```bash
cd /VbApi
```
6. Run the project
```bash
dotnet run
```
<hr>

# Endpoints
<details>
<summary>Click to display endpoints</summary>
<img width="898" alt="Endpoints" src="https://github.com/300-Akbank-Net-Bootcamp/aw-3-CanberkTimurlenk/assets/18058846/fbd21d99-3ad6-4062-a896-4c760384347c">  
</details>



[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/qoQg5l3x)
