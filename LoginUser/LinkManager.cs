using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginUser
{
    public class LinkManager
    {
        private string _connectionString;
        public LinkManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Link AddLink(Link l)
        {
            using (UserDataContext dataContext = new UserDataContext())
            {
                dataContext.Links.InsertOnSubmit(l);
                dataContext.SubmitChanges();

                return l;
            }
        }

        public IEnumerable<Link> GetAllLinks()
        {
            using (UserDataContext dataContext = new UserDataContext())
            {

                DataLoadOptions loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Link>(l => l.Uploads);
                loadOptions.LoadWith<Link>(l=>l.User);
                dataContext.LoadOptions = loadOptions;

                IEnumerable<Link> Links = dataContext.Links.ToList();

                return Links;
            }
        }

        public IEnumerable<Link> GetLinkbyUsers(string userName)
        {
            using (UserDataContext dataContext = new UserDataContext())
            {

                DataLoadOptions loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Link>(l => l.Uploads);
                loadOptions.LoadWith<Link>(l => l.User);
                dataContext.LoadOptions = loadOptions;

                User u = dataContext.Users.FirstOrDefault(user => user.UserName == userName);

                IEnumerable<Link> linksByUser = dataContext.Links.Where(l => l.Id == u.Id).ToList();

                return linksByUser;
            }
        }

        public IEnumerable<Link> GetLinkbyUserId(int UserId)
        {
            using (UserDataContext dataContext = new UserDataContext())
            {

                DataLoadOptions loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Link>(l => l.Uploads);
                loadOptions.LoadWith<Link>(l => l.User);
                dataContext.LoadOptions = loadOptions;

                User u = dataContext.Users.FirstOrDefault(user => user.Id == UserId);

                IEnumerable<Link> linksByUser = dataContext.Links.Where(l => l.UserId == u.Id).ToList();

                return linksByUser;
            }
        }

    }
}
