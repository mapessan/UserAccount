using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using UserAccount.Domain.Entity;
using UserAccount.Domain.Repository.Interfaces;

namespace UserAccount.Infra.Repository;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _userCollection;


    public UserRepository(IConfiguration configuration)
    {
        var mongoClient = new MongoClient(
            configuration["ConnectionString"]);

        var mongoDatabase = mongoClient.GetDatabase(
            configuration["DatabaseName"]);

        _userCollection = mongoDatabase.GetCollection<User>("User");
    }

    public void AddUser(User user)
    {
        //Save a new user in database
        _userCollection.InsertOneAsync(user);
    }

    public User GetUserByEmail(string email)
    {
        var user = _userCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

        return user.Result;
    }
}

