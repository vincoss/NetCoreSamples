Based on

https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-2.2&tabs=visual-studio

# Mongo db configuration

- Create database path
mongod --dbpath C:\_Data\Database\Bookstore

- use mongo command line
mongo

- Create database if not exists
use BookstoreDb

- Create a Books collection using following command:
db.createCollection('Books')

- Define a schema for the Books collection and insert two documents using the following command:
db.Books.insertMany([{'Name':'Design Patterns','Price':54.93,'Category':'Computers','Author':'Ralph Johnson'}, {'Name':'Clean Code','Price':43.15,'Category':'Computers','Author':'Robert C. Martin'}])

- View the documents in the database using the following command:
db.Books.find({}).pretty()