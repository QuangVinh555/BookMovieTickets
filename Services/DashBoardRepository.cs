using BookMovieTickets.Data;
using BookMovieTickets.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMovieTickets.Services
{
    public class DashBoardRepository : IDashBoardRepository
    {
        private readonly BookMovieTicketsContext _context;

        public DashBoardRepository(BookMovieTicketsContext context)
        {
            _context = context;
        }

        public List<MessageVM> GetAllMonthByCinemaName(int cinemaNameId)
        {
            try
            {
                List<MessageVM> list = new List<MessageVM>();
                var _cinemaNames = _context.DashBoardByCinemaNames.FromSqlRaw($@"
               SELECT cn.name AS CinemaName, Months.Month as Month, COALESCE(Count(bkd.id), 0) AS Count
                FROM (
                    SELECT 1 AS Month
                    UNION SELECT 2
                    UNION SELECT 3
                    UNION SELECT 4
                    UNION SELECT 5
                    UNION SELECT 6
                    UNION SELECT 7
                    UNION SELECT 8
                    UNION SELECT 9
                    UNION SELECT 10
                    UNION SELECT 11
                    UNION SELECT 12
                ) AS Months
                CROSS JOIN Cinema_Name AS cn
                LEFT JOIN Show_Time AS st ON cn.id = st.cinema_name_id
                LEFT JOIN Book_Ticket AS bk ON st.id = bk.show_time_id
                LEFT JOIN Book_Ticket_Detail AS bkd ON bk.id = bkd.book_ticket_id
                    AND (MONTH(bk.created_at) = Months.Month OR bk.created_at IS NULL)
                WHERE cn.id = {cinemaNameId}
                    AND (bk.created_at >= DATEADD(MONTH, -11, DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0))
                    OR bk.created_at IS NULL)
                GROUP BY cn.name, Months.Month
                ORDER BY Months.Month
            ").ToList();
                foreach (var item in _cinemaNames)
                {
                    var _cinemaName = new MessageVM
                    {
                        Message = "Lấy dữ liệu thành công",
                        Data = item
                    };
                    list.Add(_cinemaName);
                }
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public List<MessageVM> GetAllMovie()
        {
            try
            {
                List<MessageVM> list = new List<MessageVM>();
                var _movieDashboard = _context.DashBoards.FromSqlRaw($@"
                SELECT mv.id as Id, mv.name as MovieName, COUNT(bkd.id) AS Count
                FROM Movie AS mv
                INNER JOIN Book_Ticket AS bk ON mv.id = bk.movie_id
                INNER JOIN Book_Ticket_Detail AS bkd ON bk.id = bkd.book_ticket_id
                WHERE mv.premiere_date <= GETDATE()
                GROUP BY mv.name, mv.id;
            ").ToList();
                foreach (var item in _movieDashboard)
                {
                    var _movie = new MessageVM
                    {
                        Message = "Lấy dữ liệu thành công",
                        Data = item
                    };
                    list.Add(_movie);
                }
                return list;
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            
        }
    }
}
