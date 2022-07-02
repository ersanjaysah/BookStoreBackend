using BussinessLayer.Interface;
using DatabaseLayer.Model;
using ReposatoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class BookBL : IBookBL
    {
        IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        public BookModel AddBook(BookModel book)
        {
            try
            {
                return this.bookRL.AddBook(book);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
                return this.bookRL.GetBookByBookId(BookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateBook(int BookId, BookModel updateBook)
        {
            try
            {
                return this.bookRL.UpdateBook(BookId, updateBook);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
