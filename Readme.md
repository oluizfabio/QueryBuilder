# Query Generator

#### Build Queries from JSON

## Features

- Turn a JSON to a SQL Query

## Tech

Query Generator uses a number of open source projects to work properly:

- .NET Core 3.1

## Installation

Query Generator requires [.NET Core](https://github.com/dotnet/core) 3.1 to run.

Optional [Visual Studio](https://visualstudio.microsoft.com/pt-br/)

```sh
dotnet build
dotnet run
```

### Documentation

#### Input

Select specific Columns

```JSON
{
  "table": "People",
  "fields": [ "name", "age" ]
}
```

#### Output

```SQL
SELECT People.name, People.age
FROM People
```

#### Input

Select with conditions

```JSON
{
  "table": "People",
  "fields": [ "name", "age" ],
  "conditions": [
    {
      "operator": "<",
      "column": "age",
      "value": "60"
    },
    {
      "operator": "NOT LIKE",
      "column": "name",
      "value": "Peter%",
      "logicalOperator": "AND"
    }
  ]
}
```

#### Output

```SQL
SELECT People.name, People.age
FROM People
WHERE  age  < 60
AND NOT name  LIKE 'Peter%'
```

#### Input

Select with Subqueries

```JSON
{
  "table": "People",
  "fields": [ "name", "age" ],
  "conditions": [
    {
      "column": "age",
      "operator": "= ALL",
      "query": {
        "table": "Orders",
        "fields": [ "OrderID", "age" ],
        "conditions": [
          {
            "operator": "<",
            "column": "age",
            "value": "60"
          },
          {
            "operator": "=",
            "column": "name",
            "value": "Peter",
            "logicalOperator": "AND"
          }
        ]
      }
    }
  ]
}
```

#### Output

```SQL
SELECT People.name, People.age
FROM People
WHERE  age = ALL (SELECT Orders.OrderID, Orders.age
FROM Orders
WHERE  age  < 60
AND name  = 'Peter'
)
```

#### Input

Select with Joins

```JSON
{
  "table": "Product",
  "fields": [ "Id", "Name", "Price" ],
  "conditions": [
    {
      "operator": ">",
      "column": "Price",
      "value": "14"
    },
    {
      "operator": "=",
      "column": "Name",
      "value": "Test"
    }
  ],
  "queryJoins": [
    {
      "table": "HistoryPrice",
      "column": "ProductId",
      "relatedTable": "Product",
      "relatedColumn": "Id",
      "clause": "INNER"
    }
  ]
}
```

#### Output

```SQL
SELECT Product.Id, Product.Name, Product.Price
FROM Product
INNER JOIN HistoryPrice On Product.Id = HistoryPrice.ProductId
WHERE  Price  > 14
 Name  = 'Test'
```
