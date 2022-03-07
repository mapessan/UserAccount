db.createUser(
    {
      user : "matheus",
      pwd : "matheus123",
      roles : [
        {
                    role: "readWrite",
                    db: "accountdb"
        }
        ]
    });