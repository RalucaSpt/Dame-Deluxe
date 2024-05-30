### Supermarket Management Application

This project is a supermarket management application developed using C#, WPF framework, and SQLServer for the database. The application follows the MVVM (Model-View-ViewModel) architectural pattern.

### Data Stored:
- **Products:** Name, barcode, category, manufacturer.
- **Manufacturers:** Name, country of origin.
- **Product Categories:** Food, clothing, stationery, etc.
- **Product Stocks:** Quantity, unit of measure, supply date, expiration date, purchase price, selling price.
- **Users:** Username, password, user type.
- **Receipts:** Receipt date, cashier, list of products sold with quantities and subtotals, total amount.
- **Offers (optional):** Reason for offer (product expiration/stock liquidation), product, discount percentage, start date, end date.

### User Types:
- **Administrator:** Adds, modifies, deletes, and views users, categories, manufacturers, products, stocks, etc. Searches data.
- **Cashier:** Searches products, issues and views receipts.

### Functionalities for Administrator:
- CRUD operations for users, categories, manufacturers, products, stocks, etc. Data deletion is logical.
- Manual entry of purchase price for stocks; selling price is automatically calculated.
- Purchase price of a product cannot be modified after entry date; only selling price can be modified.
- Form field validations.
- Additional actions:
  1. Listing products brought by a manufacturer by category.
  2. Displaying the total value for each product category in the supermarket.
  3. Viewing daily income for a selected user in a selected month.
  4. Displaying data from the largest receipt of the day.

### Optional Functionality – Offers:
- Calculation of discounts for products nearing expiration.
- Setting price offers for stock liquidation.
- Handling situations where a product has both expiration and liquidation offers.

### Functionalities for Cashier:
- Product search by name, barcode, expiration date, manufacturer, category.
- Issuing and viewing receipts.

![image](https://github.com/RalucaSpt/SuperMart-Manager/assets/147080664/b7c1f074-fe8f-4be5-a4b4-ae014153abdf)
![image](https://github.com/RalucaSpt/SuperMart-Manager/assets/147080664/5ef193a8-a289-4794-a7af-b159526ae4cf)
![image](https://github.com/RalucaSpt/SuperMart-Manager/assets/147080664/e8d5bfe2-933b-47ae-a88b-8f3249dea865)
![image](https://github.com/RalucaSpt/SuperMart-Manager/assets/147080664/a7a4c8ce-8d71-4a16-a69c-d032dd70fc13)
![image](https://github.com/RalucaSpt/SuperMart-Manager/assets/147080664/14ef5cf0-acfc-401e-8202-95de7d82cf02)

