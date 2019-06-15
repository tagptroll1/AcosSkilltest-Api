# Programming-Task: Todo App  

## API
Note: Most endpoints require the Authorization header with a token provided by the api from /user/authenticate.  Endpoints marked with **Anonymous** does not require the token.

### /User
TODO: Write this.

#### POST /user/register **Anonymous**
Registers a user on the server.  
Requires a json form in body with Email, Username and Password.  
The response returns the Location header with an URL to where this user can get GET requested in the future.  


**Returns:**  
* 201 Created - Account was successfully created, contains the created user.
* 400 Bad Request - Something went wrong creating the account.
  * Either username is already taken, or password is too weak.
* 401 Unauthorized - Token is invalid

**Exmaple usage:**  
```http
POST http://localhost:5000/user/register
Content-Type: application/json

{
    "Email": "test@bob.com",
    "Username": "bob123",
    "Password": "bob123"
}
```

**Example response:**  
```http
HTTP/1.1 201 Created
Connection: close
Date: Fri, 14 Jun 2019 19:52:13 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Location: https://localhost:5001/User/0

{
  "id": 0,
  "email": "test@bob.com",
  "username": "bob123",
  "password": "bob123"
}
```
--------

#### POST /user/authenticate **Anonymous**
Authenticates a user by checking its username and password with the stored password hash.  When the process is complete, and the user is authenticated a Token will be returned.  Use this token in the `Authorization` header to access most other endpoints.  

**Returns:**  
* 200 Ok - User was authenticated successfully. Returns Id, username and Token
* 400 Bad Request - Authentication failed, see error message.
* 401 Unauthorized - Token is invalid

**Example usage:**  
```http
POST http://localhost:5000/user/authenticate
Content-Type: application/json

{
    "Email": "test@bob.com",
    "Username": "bob123",
    "Password": "bob123"
}
```

**Example response:**  
```http
HTTP/1.1 200 OK
Connection: close
Date: Fri, 14 Jun 2019 19:57:10 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

{
  "id": 1,
  "username": "bob123",
  "token": "<token>"
}
```
--------

#### GET /user
Returns all created users, requires authentication.  
Also removes the password from responses.

**Returns:**  
* 200 Ok
* 401 Unauthorized - Token is invalid
* 404 Not Found

**Example usage:**  
```http
GET http://localhost:5000/user
Authorization: Bearer <token>
```

**Example response:**
```http
HTTP/1.1 200 OK
Connection: close
Date: Fri, 14 Jun 2019 20:01:43 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

[
  {
    "id": 1,
    "email": "test@bob.com",
    "username": "bob123",
    "password": null
  },
  {
    "id": 2,
    "email": "Tom@hotmail.com",
    "username": "tommy123",
    "password": null
  }
]
```
--------

#### GET /user/{id}
Gets a user by it's ID.

**Returns:**
* 200 Ok
* 401 Unauthorized - Token is invalid
* 404 Not Found

**Example usage:**
```http
GET http://localhost:5000/user/1
Authorization: Bearer <token>

```
**Example response:**
```http
HTTP/1.1 200 OK
Connection: close
Date: Fri, 14 Jun 2019 20:02:55 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

{
  "id": 1,
  "email": "test@bob.com",
  "username": "bob123",
  "password": null
}
```
--------

#### PUT /user/{id}
Updates a users username & password.  The Id parameter is the user to be updated, and the points to be updates needs to be passed as json in the body.

**Returns:**
* 200 Ok - Update was successfull.
* 400 Bad request - Update was unsuccessfull, could be taken username or weak password.  Check response message
* 401 Unauthorized - Token is invalid

**Example usage:**
```http
PUT http://localhost:5000/user/1
Authorization: Bearer <token>
Content-Type: application/json

{
    "Username": "bob123455",
    "Password": "lolhaah"
}

```
**Example response:**
```http
HTTP/1.1 200 OK
Connection: close
Date: Fri, 14 Jun 2019 20:04:24 GMT
Server: Kestrel
Content-Length: 0
```
--------

#### DELETE /user/{id}
Deletes a user by id

**Returns:**
* 200 Ok - Deletion was successfull.
* 401 Unauthorized - Token is invalid

**Example usage:**
```http
DELETE  http://localhost:5000/user/1
Authorization: Bearer <token>
```

**Example response:**
```http
HTTP/1.1 200 OK
Connection: close
Date: Fri, 14 Jun 2019 20:07:15 GMT
Server: Kestrel
Content-Length: 0
```

--------

### /whiteboard

#### GET /whiteboard/{id}
Gets a whiteboard object by the `id` parameter. 
This endpoint will map the User returned to just Id, Email and Username.

**Returns:**
* 200 Ok - Got the Whiteboard
* 401 Unauthorized - Token is invalid
* 404 Not Found - Whiteboard or user id does not exist  

**Example usage:**  
```http
GET http://localhost/whiteboard/1
Authorization: Bearer <Token>
```

**Example result:**
```http
HTTP/1.1 200 OK
Connection: close
Date: Fri, 14 Jun 2019 18:19:43 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

{
  "id": 1,
  "title": "Must be done",
  "owner": {
    "id": 1,
    "email": "test@bob.com",
    "username": "bob123",
    "password": null
  }
}
```  
--------


#### GET /whiteboard/user/{id}
Gets an arroy of whiteboard objects by the `id` of a user.  
This endpoint will map the User returned to just Id, Email and Username.

**Returns:**
* 200 Ok - Got all the whiteboards successfully.
* 401 Unauthorized - Token is invalid
* 404 Not Found - User was not found, or no Whiteboards registered to user.

**Example usage:**  
```http
GET http://localhost/whiteboard/user/1
Authorization: Bearer <Token>
```

**Example result:**
```http
HTTP/1.1 200 OK
Connection: close
Date: Fri, 14 Jun 2019 18:30:06 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

[
  {
    "id": 1,
    "title": "Must be done",
    "owner": {
      "id": 1,
      "email": "test@bob.com",
      "username": "bob123",
      "password": null
    }
  }
]
```
--------


#### GET /whiteboard
Gets an arroy of all whiteboards available.  
This endpoint will map the User returned to just Id, Email and Username.

**Returns:**
* 200 Ok - Whiteboards were retrieved successfully.
* 401 Unauthorized - Token is invalid
* 404 Not Found - No whiteboards were found.

**Example usage:**  
```http
GET http://localhost/whiteboard
Authorization: Bearer <Token>
```

**Example result:**
```http
HTTP/1.1 200 OK
Connection: close
Date: Fri, 14 Jun 2019 18:34:07 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

[
  {
    "id": 1,
    "title": "Must be done",
    "owner": {
      "id": 1,
      "email": "test@bob.com",
      "username": "bob123",
      "password": null
    }
  }
]
```
--------


#### POST /whiteboard/new
Creates a new Whiteboard for the User specified in the Owner field.

**Parameters:**
* Title - The title / category of the Whiteboard.  
* Owner - Id, Username or Email of the User owning the Whiteboard.  

**Returns:**
* 200 OK - With the created Whiteboard  
* 400 Bad Request - The user was not found or the Owner field is empty
* 401 Unauthorized - Token is invalid


This endpoint will map the User returned to just Id, Email and Username.

**Example usage:**  
```http
POST http://localhost/whiteboard/new
Authorization: Bearer <Token>
Content-Type: application/json

{
    "Title": "Must be done",
    "Owner": "bob123"
}
```

**Example result:**
```http
HTTP/1.1 201 Created
Connection: close
Date: Fri, 14 Jun 2019 18:39:46 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Location: https://localhost:5001/Whiteboard/0

{
  "id": 0,
  "title": "Must be done",
  "owner": "bob123"
}
```
--------


#### PUT /whiteboard/id
Updates an existing whiteboard  

**Parameters:**
* Object representing new Whiteboard (Body)

**Returns:**
* 200 Ok
* 401 Unauthorized - Token is invalid

--------


#### DELETE /whiteboard/id
Deletes a Whiteboard by id

**Returns:**
* 200 Ok
* 401 Unauthorized - Token is invalid

--------

### /postit

#### GET /postit/{id}
Gets a postit object by the `id` parameter. 
This endpoint will map the User returned to just Id, Email and Username.

**Returns:**
* 200 Ok - Got the Postit
* 401 Unauthorized - Token is invalid
* 404 Not Found - postit does not exist  

**Example usage:**  
```http
GET http://localhost/postit/1
Authorization: Bearer <Token>
```

**Example result:**