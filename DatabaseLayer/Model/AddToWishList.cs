using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Model
{
    public class AddToWishList
    {
        
        //public int WishlistId { get; set; }
        //public int userId { get; set; }
        //public int bookId { get; set; }
        //public AddBookModel Bookmodel { get; set; }

        public int WishListId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
         public BookModel bookModel { get; set; }
    }
}
