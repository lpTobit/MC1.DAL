using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MC1.DAL
{
    class UserRepository
    {
        private List<User> datas;
        private string path = "Data/users.json";
        public UserRepository()
        {
            datas = new List<User>();

            FileInfo fi = new FileInfo(path);
            if (!fi.Directory.Exists)
                fi.Directory.Create();
        }         
        public void Add(User obj)
        {
            foreach (var data in datas)
                if (data.Email.Equals(obj.Email, StringComparison.InvariantCultureIgnoreCase))
                    throw new DuplicateWaitObjectException("Email already exists !");
            datas.Add(obj);
        }
        public void Set(User oldObj, User newObj)
        {
            var oldindex = -1;
            for (int i = 0; i < datas.Count; i++)
                if (datas[i].Email.Equals(oldObj.Email, StringComparison.OrdinalIgnoreCase))
                    oldindex = i;
            if (oldindex < 0)
                throw new KeyNotFoundException("Email not found !");

            var newindex = -1;
            for (int i = 0; i < datas.Count; i++)
                if (datas[i].Email.Equals(oldObj.Email, StringComparison.OrdinalIgnoreCase))
                    newindex = i;


            if (newindex >= 0 && newindex != oldindex)
                throw new KeyNotFoundException("Email not found !");

            datas[oldindex] = newObj;
        }
        public void Delete(User obj)
        {
            var index = -1;
            for (int i = 0; i < datas.Count; i++)
                if (datas[i].Email.Equals(obj.Email, StringComparison.OrdinalIgnoreCase))
                     index = i;
            if (index >= 0)
                datas.RemoveAt(index);
        }
        public User Login(string email, string password)
        {
            foreach (var data in datas)
                if (data.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                    data.Password.Equals(password))
                    return data;
           return null;
        }
        public List<User> FindByName(string value)
        {
            List<User> list = new List<User>();
            foreach (var data in datas)
                if (data.Fullname.ToLower().Contains(value.ToLower()))
                    list.Add(data);
            return list;
        }
        public void save(string path)
        {

        }
    }
}
