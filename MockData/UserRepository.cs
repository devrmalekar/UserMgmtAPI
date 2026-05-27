using UserManagement.Models;

namespace UserManagement.MockData
{
    public class UserRepository
    {
        private int _nextId = 11; // Starting ID for new users
        private readonly object _lock = new();
        public List<User> Users { get; set; }

        public UserRepository()
        {
            Users = new();
            for (int i = 1; i <= 10; i++)
            {
                Users.Add(
                    new User
                    {
                        Id = i,
                        Name = $"User{i}",
                        Email = $"user{i}@example.com",
                    }
                );
            }
        }

        public Task<List<User>> GetAllUsers()
        {
            lock (_lock)
            {
                return Task.FromResult(Users.ToList());
            }
        }

        public Task<User?> GetUserById(int id)
        {
            lock (_lock)
            {
                var user = Users.FirstOrDefault(u => u.Id == id);
                return Task.FromResult(user);
            }
        }

        public async Task<User> AddUser(User user)
        {
            user.Id = _nextId++;
            lock (_lock)
            {
                Users.Add(user);
            }
            return await Task.FromResult(user);
        }

        public async Task<User> UpdateUser(int id, User updatedUser)
        {
            var existingUser = await GetUserById(id);
            if (existingUser != null && existingUser.Id == id)
            {
                lock (_lock)
                {
                    existingUser.Name = updatedUser.Name;
                    existingUser.Email = updatedUser.Email;
                }
                return await Task.FromResult(existingUser);
            }
            return await Task.FromResult(existingUser!);
        }

        public async Task<User?> DeleteUser(int id)
        {
            var user = await GetUserById(id);
            if (user != null && user.Id == id)
            {
                lock (_lock)
                {
                    Users.Remove(user);
                }
                return await Task.FromResult(user);
            }
            return await Task.FromResult(user);
        }
    }
}
