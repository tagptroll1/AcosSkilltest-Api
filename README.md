# Programming-Task: Todo App  

## API
--------
### /User endpoint

--------
### /Whiteboard endpoint

#### GET /Whiteboard/{id}
Gets a whiteboard object by the `id` parameter.  
Returns `200 OK` or `404 Not Found` based on the existence of the Whiteboard  
This endpoint will map the User returned to just Id, Email and Username.

Example usage:  
```http
GET http://localhost/whiteboard/1
Authorization: <Token>
```

Example result:
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

#### GET /Whiteboard/user/{id}
Gets an arroy of whiteboard objects by the `id` of a user.  
Returns `200 OK` or `404 Not Found` based on the existence of the Whiteboards and User.  
This endpoint will map the User returned to just Id, Email and Username.

Example usage:  
```http
GET http://localhost/whiteboard/user/1
Authorization: <Token>
```

Example result:
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

#### GET /Whiteboard
Gets an arroy of all whiteboards available.  
Returns `200 OK` or `404 Not Found` based on the existence of the Whiteboards
This endpoint will map the User returned to just Id, Email and Username.

Example usage:  
```http
GET http://localhost/whiteboard
Authorization: <Token>
```

Example result:
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

#### POST /Whiteboard/new
Creates a new Whiteboard for the User specified in the Owner field.

**Parameters:**
* Title - The title / category of the Whiteboard.  
* Owner - Id, Username or Email of the User owning the Whiteboard.  

**Returns:**
* 200 OK - With the created Whiteboard  
* 400 Bad Request - The user was not found or the Owner field is empty


This endpoint will map the User returned to just Id, Email and Username.

Example usage:  
```http
POST http://localhost/whiteboard/new
Authorization: <Token>
Content-Type: application/json

{
    "Title": "Must be done",
    "Owner": "bob123"
}
```

Example result:
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

#### PUT /Whiteboard/id
Updates an existing whiteboard  

**Parameters:**
* Object representing new Whiteboard (Body)

#### DELETE /Whiteboard/id
Deletes a Whiteboard by id
