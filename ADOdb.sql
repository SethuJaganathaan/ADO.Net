CREATE TABLE Users
(
UserId INT IDENTITY(1,1) PRIMARY KEY,
UserName VARCHAR(20),
DOB DATE,
DepartmentId INT FOREIGN KEY (DepartmentId) REFERENCES Department (DepartmentId)
)
DROP TABLE Users
INSERT INTO Users VALUES ('Naruto','01/01/2000',200)

CREATE TABLE Department
(
DepartmentId INT IDENTITY(200,1) PRIMARY KEY,
DepartmentName VARCHAR(20),
DepartmentIncharge VARCHAR(30)
)
DROP TABLE Department
INSERT INTO Department VALUES ('Team7','Kakashi')

CREATE PROCEDURE InsertUpdateDepartment
(
@DepartmentId INT,
@DepartmentName VARCHAR(20),
@DepartmentIncharge VARCHAR(30)
)
AS 
BEGIN
     IF EXISTS(SELECT * FROM Department WHERE DepartmentId = @DepartmentId)
	    BEGIN
		    UPDATE Department SET DepartmentName = @DepartmentName,DepartmentIncharge = @DepartmentIncharge WHERE DepartmentId = @DepartmentId
		    SELECT 'Department Details Updated' AS Result
        END
     ELSE
	    BEGIN
		    INSERT INTO Department (DepartmentName,DepartmentIncharge) VALUES (@DepartmentName,@DepartmentIncharge)
			SELECT 'Department Added' AS Result
		END  
END	 
DROP PROCEDURE InsertUpdateDepartment
EXEC InsertUpdateDepartment 0,'DemonSlayer','Rengoku'

SELECT * FROM Users
SELECT * FROM Department
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE InsertUpdateUsers
(
@UserId INT,
@UserName VARCHAR(20),
@DOB DATE,
@DepartmentId INT
)
AS
BEGIN
    IF EXISTS(SELECT * FROM Users WHERE UserId = @UserId)
	   BEGIN
	       UPDATE Users SET UserName = @UserName WHERE UserId = @UserId
		   SELECT 'User Updated' AS Result
       END
    ELSE
	   BEGIN
	       INSERT INTO Users (UserName,DOB,DepartmentId) VALUES (@UserName,@DOB,@DepartmentId)
		   SELECT 'User Added' AS Result
       END
END

SELECT * FROM Users
DROP PROCEDURE InsertUpdateUsers
EXEC InsertUpdateUsers 0,'Sasuke','02-02-2000',200

--------------------------------------------------------------------------------------------------

ALTER PROCEDURE GETandDELETEuser
(
@UserId INT,
@Method VARCHAR(20)
)
AS
BEGIN
    IF (@Method = 'Get')
	  BEGIN
	      SELECT * FROM Users WHERE UserId = @UserId
	  END
    ELSE IF (@Method = 'Delete')
	  BEGIN
	      DELETE FROM Users WHERE UserId = @UserId
		  SELECT * FROM Users
      END
END    

EXEC GETandDELETEuser 1,'Get'

CREATE PROCEDURE GETandDELETEdepartment
(
@DepartmentId INT,
@Method VARCHAR(20)
)
AS 
BEGIN
    IF ( @Method = 'GET')
	   BEGIN
	       SELECT * FROM Department WHERE DepartmentId = @DepartmentId
	   END
    ELSE IF ( @Method = 'Delete')
	   BEGIN
	       DELETE FROM Department WHERE DepartmentId = @DepartmentId
	   END
END

EXEC GETandDELETEdepartment 200 ,'GET'

  