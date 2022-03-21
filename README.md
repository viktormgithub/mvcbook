### Тестовое задание MVC Book
**Виктор Митяев** 
<br>
*rabota@viktorm.ru*
---
Хотелось бы:
- сделать вызов сервиса, который содержит бизнес-логику. 
- Сервис работает с репозиторием ,
- логику работы с базой перенести в репозитории

###Cоздание БД . Например список книг в домашней библиотеке:

CREATE DATABASE BOOKBASE;

Создание таблицы в БД. БД создавали руками (TODO: нужно добавить NOT NULL на большинство полей)

USE BOOKBASE;

CREATE TABLE Books (
Id INT IDENTITY(1,1) PRIMARY KEY,
Title NVARCHAR(255),
Author NVARCHAR(255),
Year SMALLINT,
Publisher NVARCHAR(255),
Annotation NVARCHAR(4000),
ISBN10 VARCHAR(10),
ISBN13 VARCHAR(13),
Content XML
)

### Результат:
dbo.Books в BOOKBASE

---

*Добавим несколько записей в базу данных для тестирования*

INSERT INTO [BOOKBASE].[dbo].[Books](Title, Author, Year, Publisher, Annotation, ISBN10, ISBN13, Content)
VALUES ('Title 1', 'Autor 1', 1961, 'USSR', 'Amazing book 1', '1234567890', '1234567890123', '<content></content>');

INSERT INTO [BOOKBASE].[dbo].[Books](Title, Author, Year, Publisher, Annotation, ISBN10, ISBN13, Content)
VALUES ('Title 2', 'Autor 2', 1962, 'USSR', 'Amazing book 2', '1234567891', '1234567890124', '<content></content>');

INSERT INTO [BOOKBASE].[dbo].[Books](Title, Author, Year, Publisher, Annotation, ISBN10, ISBN13, Content)
VALUES ('Title 3', 'Autor 3', 1963, 'USA', 'Amazing book 3', '1234567892', '1234567890125', '<content></content>');



Создание хранимых процедур

### Хранимая процедура для SELECT:

USE BOOKBASE;<br>
GO<br>
CREATE PROCEDURE SelectBooks AS<br>
BEGIN<br>
SELECT id, title, author, year, publisher, annotation, isbn10, isbn13, content
FROM Books<br>
END;<br>



USE BOOKBASE;<br>
GO<br>
CREATE PROCEDURE SelectBookById<br>
@id INT<br>
AS<br>
BEGIN<br>
SELECT id, title, author, year, publisher, annotation, isbn10, isbn13, content
FROM Books<br>
WHERE id = @id<br>
END;<br>


### Хранимая процедура для Insert:

USE BOOKBASE;<br>
GO<br>
CREATE PROCEDURE AddBook<br>
@title NVARCHAR(255),<br>
@author NVARCHAR(255),<br>
@year SMALLINT,<br>
@publisher NVARCHAR(255),<br>
@annotation NVARCHAR(4000),<br>
@isbn10 VARCHAR(10),<br>
@isbn13 VARCHAR(13),<br>
@content XML<br>
AS<br>
BEGIN<br>
INSERT INTO Books(Title, Author, Year, Publisher, Annotation, ISBN10, ISBN13, Content)<br>
VALUES(@title, @author, @year, @publisher, @annotation, @isbn10, @isbn13, @content)<br>
END;<br>


### Хранимая процедура для Update:

USE BOOKBASE;<br>
GO<br>
CREATE PROCEDURE UpdateBookById<br>
@id INT,<br>
@title NVARCHAR(255),<br>
@author NVARCHAR(255),<br>
@year SMALLINT,<br>
@publisher NVARCHAR(255),<br>
@annotation NVARCHAR(4000),<br>
@isbn10 VARCHAR(10),<br>
@isbn13 VARCHAR(13),<br>
@content XML<br>
AS<br>
BEGIN<br>
UPDATE Books<br>
SET Title = @title, Author = @author, Year = @year, Publisher = @publisher, Annotation = @annotation, ISBN10 = @isbn10, ISBN13 = @isbn13, Content = @content<br>
WHERE id = @id;<br>
END;<br>


### Хранимая процедура для Delete:

USE BOOKBASE;<br>
GO<br>
CREATE PROCEDURE DeleteBookById<br>
@id INT<br>
AS<br>
BEGIN<br>
DELETE Books<br>
WHERE id = @id;<br>
END;<br>
