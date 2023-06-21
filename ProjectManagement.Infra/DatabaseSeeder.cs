using Microsoft.EntityFrameworkCore;
using ProjectManagement.Infra.Models;
using System;

namespace ProjectManagement.Infra
{
    public static class DatabaseSeeder
    {
        public static void SeedData(DbContext dbContext)
        {
            if (dbContext is null)
                throw new ArgumentNullException(nameof(dbContext));

            SeedCards(dbContext);
        }

        private static PersonModel[] SeedPeople(DbContext dbContext, IEnumerable<Guid> ids)
        {
            static string GenerateRandomName()
            {
                string[] names = { "John Doe", "Jane Smith", "Michael Johnson", "Emily Davis", "Daniel Wilson", "Olivia Brown" };
                return names[new Random().Next(names.Length)];
            }

            var people = new PersonModel[ids.Count()];

            for (int i = 0; i < ids.Count(); i++)
                people[i] = new PersonModel { Id = ids.ElementAt(i), Name = GenerateRandomName() };

            dbContext.Set<PersonModel>().AddRange(people);
            dbContext.SaveChanges();

            return people;
        }

        private static ListModel[] SeedLists(DbContext dbContext, IEnumerable<Guid> ids)
        {
            static ListModel[] GenerateLists(IEnumerable<Guid> ids)
            {
                static string GenerateRandomName()
                {
                    string[] names = { "Groceries", "Work Tasks", "Personal Projects", "Shopping", "Books to Read", "Movies to Watch" };
                    return names[new Random().Next(names.Length)];
                };

                static int GenerateRandomPosition() => new Random().Next(1, 101); // Generate random positions between 1 and 100

                var count = ids.Count();
                var lists = new ListModel[count];

                for (int i = 0; i < count; i++)
                {
                    lists[i] = new ListModel
                    {
                        Id = ids.ElementAt(i),
                        Name = GenerateRandomName(),
                        Position = GenerateRandomPosition()
                    };
                }

                return lists;
            }

            // Seed data
            var lists = GenerateLists(ids);

            dbContext.Set<ListModel>().AddRange(lists);
            dbContext.SaveChanges();

            return lists;
        }

        private static void SeedCards(DbContext dbContext)
        {
            static CardModel[] GenerateCards(int count)
            {
                static string GenerateRandomTitle()
                {
                    string[] titles = { "Task 1", "Task 2", "Task 3", "Task 4", "Task 5", "Task 6" };
                    return titles[new Random().Next(titles.Length)];
                }

                static string GenerateRandomDescription()
                {
                    string[] descriptions = { "Description 1", "Description 2", "Description 3", "Description 4", "Description 5" };
                    return descriptions[new Random().Next(descriptions.Length)];
                }

                static int GenerateRandomRanking() => new Random().Next(1, 11); 
                static Guid GenerateRandomId() => Guid.NewGuid();
                static bool GenerateRandomIsCompleted() => new Random().Next(2) == 0;

                static DateTime GenerateRandomDate()
                {
                    // Generate random date within a specific range (e.g., within the last year)
                    var startDate = DateTime.Now.AddYears(-1);
                    var timeSpan = DateTime.Now - startDate;
                    var randomTimeSpan = new TimeSpan((long)(new Random().NextDouble() * timeSpan.Ticks));

                    return startDate + randomTimeSpan;
                }

                var cards = new CardModel[count];

                for (int i = 0; i < count; i++)
                {
                    cards[i] = new CardModel
                    {
                        Id = Guid.NewGuid(),
                        Title = GenerateRandomTitle(),
                        Description = GenerateRandomDescription(),
                        Ranking = GenerateRandomRanking(),
                        ListId = GenerateRandomId(),
                        PersonId = GenerateRandomId(),
                        IsCompleted = GenerateRandomIsCompleted(),
                        Date = GenerateRandomDate()
                    };
                }

                return cards;
            }

            if (dbContext.Set<CardModel>().Any())
                return;

            var cards = GenerateCards(10);
            var people = SeedPeople(dbContext, cards.Select(x => x.PersonId));
            var lists = SeedLists(dbContext, cards.Select(x => x.ListId));

            foreach (var card in cards)
            {
                card.Person = people.Where(x => x.Id == card.PersonId).First();
                card.List = lists.Where(x => x.Id != card.ListId).First();
            }

            dbContext.Set<CardModel>().AddRange(cards);
            dbContext.SaveChanges();
        }
    }
}
