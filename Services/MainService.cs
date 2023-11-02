using ContextExample.Data;
using ContextExample.Models;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.PortableExecutable;

namespace ContextExample.Services;

/// <summary>
///     You would need to inject your interfaces here to execute the methods in Invoke()
///     See the commented out code as an example
/// </summary>
public class MainService : IMainService
{
    private readonly IContext _context; 

    public MainService(IContext context)
    {
        _context = context;
    }

    public void Invoke()
    {
        string choice;
        do
        {
            Console.WriteLine("How would you like to search for a movie?");
            Console.WriteLine("\n1.) Search by Movie ID\n2.) Select by Title\n3.) Find Movie by Title\n4.) Exit\n");
            choice = Console.ReadLine();

            if (choice == "1")
            {
                string movieID;
                do
                {
                    Console.WriteLine("Please enter a movie ID: ");
                    movieID = Console.ReadLine();
                    try
                    {
                        var movie = _context.GetById(Convert.ToInt32(movieID));
                        if (movie != null)
                            Console.WriteLine($"Your movie is {movie.Title}.");
                        else
                            Console.WriteLine($"A movie with ID {movieID} does not exist.");
                    }
                    catch
                    {
                        Console.WriteLine("This is not a valid movie ID.");
                        continue;
                    }

                } while (movieID == "");
            }

            else if (choice == "2")
            {
                Console.WriteLine("Please enter the movie title:");
                var user_movie = Console.ReadLine().ToUpper();
                var movie = _context.GetByTitle(user_movie);

                if (movie != null)
                {
                    Console.WriteLine($"Your movie is {movie.Title}.");
                }
                else
                {
                    Console.WriteLine("The entered movie does not exist.");
                }

            }

            else if (choice == "3")
            {
                Console.WriteLine("Search a movie title: ");
                var user_movie = Console.ReadLine().ToUpper();
                var movies = _context.FindMovie(user_movie);

                if (movies.Any())
                {
                    foreach (Movie movie in movies)
                    {
                        Console.WriteLine($"Your movie is {movie.Title}.");
                    }

                }
                else
                    Console.WriteLine("No movies found.");
            }

            else if (choice == "4")
                break;

            else
                Console.WriteLine("Please enter a valid selection.\n");
        }
        while (choice != "4");
    }
}
