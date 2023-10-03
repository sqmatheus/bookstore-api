using BookStore.Data;
using BookStore.Helpers;
using BookStore.Models;

namespace BookStore
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }
        public void SeedContext()
        {
            if (!_context.Books.Any())
            {
                var genre1 = new Genre()
                {
                    Name = "Fábula",
                    Slug = Slug.Slugify("Fábula"),
                    CreatedAt = DateTime.Now
                };

                var genre2 = new Genre()
                {
                    Name = "Novela",
                    Slug = Slug.Slugify("Novela"),
                    CreatedAt = DateTime.Now
                };

                var genre3 = new Genre()
                {
                    Name = "Comédia",
                    Slug = Slug.Slugify("Comédia"),
                    CreatedAt = DateTime.Now
                };

                var genre4 = new Genre()
                {
                    Name = "Juvenil",
                    Slug = Slug.Slugify("Juvenil"),
                    CreatedAt = DateTime.Now
                };

                var genre5 = new Genre()
                {
                    Name = "Fantasia",
                    Slug = Slug.Slugify("Fantasia"),
                    CreatedAt = DateTime.Now
                };

                var genre6 = new Genre()
                {
                    Name = "Ficção",
                    Slug = Slug.Slugify("Ficção"),
                    CreatedAt = DateTime.Now
                };

                var books = new List<Book>()
                {
                    new Book()
                    {
                        Title = "O Pequeno Príncipe",
                        Slug = Slug.Slugify("O Pequeno Príncipe"),
                        Description = "Le Petit Prince é uma novela do escritor, aviador aristocrata francês Antoine de Saint-Exupéry, originalmente publicada em inglês e francês em abril de 1943 nos Estados Unidos. Durante a Segunda Guerra Mundial, Saint-Exupéry foi exilado para a América do Norte",
                        Author = "Antoine de Saint-Exupéry",
                        PublicationDate = new DateTime(1943, 4, 6),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        BookGenres = new List<BookGenre>() {
                            new BookGenre() {
                                Genre = genre1
                            },
                            new BookGenre() {
                                Genre = genre2
                            },
                            new BookGenre() {
                                Genre = genre6
                            }
                        }
                    },
                    new Book()
                    {
                        Title = "Diário de um Banana I",
                        Slug = Slug.Slugify("Diário de um Banana I"),
                        Description = "Um livro de romance infantil escrito e ilustrado por Jeff Kinney. É o primeiro livro da série Diary of a Wimpy Kid. O livro é sobre um menino chamado Greg Heffley e suas tentativas de se tornar popular em seu sexto ano do ensino fundamental",
                        Author = "Jeff Kinney",
                        PublicationDate = new DateTime(2007, 4, 1),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        BookGenres = new List<BookGenre>() {
                            new BookGenre() {
                                Genre = genre3
                            },
                            new BookGenre() {
                                Genre = genre4
                            }
                        }
                    },
                    new Book()
                    {
                        Title = "Harry Potter e a Pedra Filosofal",
                        Slug = Slug.Slugify("Harry Potter e a Pedra Filosofal"),
                        Description = "O primeiro dos sete livros da série de fantasia Harry Potter, escrita por J. K. Rowling. O livro conta a história de Harry Potter, um órfão criado pelos tios que descobre, em seu décimo primeiro aniversário, que é um bruxo. No romance, são narrados seus primeiros passos na comunidade bruxa, sua entrada na Escola de Magia e Bruxaria de Hogwarts e o início de sua amizade com Ron Weasley e Hermione Granger, os quais o ajudam a enfrentar Lorde Voldemort - Lorde das Trevas.",
                        Author = "J. K. Rowling",
                        PublicationDate = new DateTime(1997, 6, 26),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        BookGenres = new List<BookGenre>() {
                            new BookGenre() {
                                Genre = genre5
                            },
                            new BookGenre() {
                                Genre = genre6
                            }
                        }
                    }
                };

                _context.Books.AddRange(books);
                _context.SaveChanges();
            }
        }
    }
}