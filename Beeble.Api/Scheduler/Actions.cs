﻿using Beeble.Data;
using Beeble.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Beeble.Api.Scheduler
{
    public class Actions
    {
        public static void DeleteExpiredRegistrations()
        {
            using (var context = new AuthContext())
            {
                var expiredReservations = context.Reservations
                    .Include(reservation => reservation.Book)
                    .Where(reservation => reservation.PickupDeadline < DateTime.Now).ToList();

                expiredReservations.Select(reservation => reservation.Book)
                    .ToList()
                    .ForEach(reservedBook => reservedBook.IsAvailable = true);

                expiredReservations.ForEach(reservation =>
                                            context.Entry(reservation).State = EntityState.Deleted);

                context.SaveChanges();
            }
        }
    }
}