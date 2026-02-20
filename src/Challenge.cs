// DESAFIO: Sistema de Playlist de Música
// PROBLEMA: Uma aplicação de streaming precisa permitir diferentes formas de navegar por
// playlists (sequencial, aleatória, por gênero, filtrada). O código atual expõe a
// estrutura interna das coleções e repete lógica de iteração em vários lugares

using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternChallenge
{
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public int DurationSeconds { get; set; }
        public int Year { get; set; }

        public Song(string title, string artist, string genre, int duration, int year)
        {
            Title = title;
            Artist = artist;
            Genre = genre;
            DurationSeconds = duration;
            Year = year;
        }

        public override string ToString()
        {
            return $"{Title} - {Artist} ({Genre}, {Year})";
        }
    }

    // Problema: Playlist expõe estrutura interna (List)
    public class Playlist
    {
        public string Name { get; set; }
        public List<Song> Songs { get; set; } // Expor List diretamente é problemático

        public Playlist(string name)
        {
            Name = name;
            Songs = new List<Song>();
        }

        public void AddSong(Song song)
        {
            Songs.Add(song);
        }
    }

    // Cliente precisa implementar diferentes formas de iteração
    public class MusicPlayer
    {
        private Playlist _playlist;
        private int _currentIndex;

        public MusicPlayer(Playlist playlist)
        {
            _playlist = playlist;
            _currentIndex = 0;
        }

        // Problema 1: Acesso direto à estrutura interna
        public void PlaySequential()
        {
            Console.WriteLine($"\n=== Tocando {_playlist.Name} (Sequencial) ===");
            
            for (int i = 0; i < _playlist.Songs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_playlist.Songs[i]}");
            }
        }

        // Problema 2: Lógica de shuffle implementada aqui
        public void PlayShuffle()
        {
            Console.WriteLine($"\n=== Tocando {_playlist.Name} (Aleatório) ===");
            
            var random = new Random();
            var shuffled = _playlist.Songs.OrderBy(x => random.Next()).ToList();
            
            for (int i = 0; i < shuffled.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {shuffled[i]}");
            }
        }

        // Problema 3: Filtros implementados no cliente
        public void PlayByGenre(string genre)
        {
            Console.WriteLine($"\n=== Tocando {_playlist.Name} (Gênero: {genre}) ===");
            
            var filtered = _playlist.Songs.Where(s => s.Genre == genre).ToList();
            
            for (int i = 0; i < filtered.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {filtered[i]}");
            }
        }

        // Problema 4: Navegação customizada requer conhecer estrutura interna
        public void PlayOldies()
        {
            Console.WriteLine($"\n=== Tocando {_playlist.Name} (Antigas) ===");
            
            var oldies = _playlist.Songs.Where(s => s.Year < 2000).OrderBy(s => s.Year).ToList();
            
            for (int i = 0; i < oldies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {oldies[i]}");
            }
        }

        // Problema 5: Diferentes coleções exigem código diferente
        public void PlayFromArray(Song[] songs)
        {
            Console.WriteLine("\n=== Tocando de Array ===");
            // Código diferente para array
            for (int i = 0; i < songs.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {songs[i]}");
            }
        }

        public void PlayFromQueue(Queue<Song> songs)
        {
            Console.WriteLine("\n=== Tocando de Fila ===");
            // Código diferente para Queue
            int count = 1;
            while (songs.Count > 0)
            {
                var song = songs.Dequeue();
                Console.WriteLine($"{count++}. {song}");
            }
        }
    }

    // Problema: Biblioteca de músicas com estrutura personalizada
    public class MusicLibrary
    {
        // Estrutura interna complexa (árvore, grafo, etc)
        private Dictionary<string, List<Song>> _songsByGenre;
        private Dictionary<string, List<Song>> _songsByArtist;

        public MusicLibrary()
        {
            _songsByGenre = new Dictionary<string, List<Song>>();
            _songsByArtist = new Dictionary<string, List<Song>>();
        }

        public void AddSong(Song song)
        {
            if (!_songsByGenre.ContainsKey(song.Genre))
                _songsByGenre[song.Genre] = new List<Song>();
            _songsByGenre[song.Genre].Add(song);

            if (!_songsByArtist.ContainsKey(song.Artist))
                _songsByArtist[song.Artist] = new List<Song>();
            _songsByArtist[song.Artist].Add(song);
        }

        // Problema: Como iterar sobre toda biblioteca sem expor estrutura?
        public Dictionary<string, List<Song>> GetSongsByGenre()
        {
            return _songsByGenre; // Expõe estrutura interna!
        }

        // Cliente precisa saber como navegar esta estrutura complexa
    }

    public class LegacyProgram
    {
        public static void RunLegacy()
        {
            Console.WriteLine("=== Sistema de Playlist ===");

            var playlist = new Playlist("Minhas Favoritas");
            playlist.AddSong(new Song("Bohemian Rhapsody", "Queen", "Rock", 354, 1975));
            playlist.AddSong(new Song("Imagine", "John Lennon", "Pop", 183, 1971));
            playlist.AddSong(new Song("Smells Like Teen Spirit", "Nirvana", "Rock", 301, 1991));
            playlist.AddSong(new Song("Billie Jean", "Michael Jackson", "Pop", 294, 1982));
            playlist.AddSong(new Song("Hotel California", "Eagles", "Rock", 391, 1976));
            playlist.AddSong(new Song("Sweet Child O' Mine", "Guns N' Roses", "Rock", 356, 1987));

            var player = new MusicPlayer(playlist);

            player.PlaySequential();
            player.PlayShuffle();
            player.PlayByGenre("Rock");
            player.PlayOldies();

            Console.WriteLine("\n=== PROBLEMAS ===");
            Console.WriteLine("✗ Estrutura interna da coleção exposta (List<Song> público)");
            Console.WriteLine("✗ Lógica de iteração repetida em múltiplos métodos");
            Console.WriteLine("✗ Cliente depende do tipo de coleção (List, Array, Queue)");
            Console.WriteLine("✗ Difícil mudar estrutura interna sem quebrar clientes");
            Console.WriteLine("✗ Não é possível iterar múltiplas coleções uniformemente");
            Console.WriteLine("✗ Não há forma padrão de pausar/retomar iteração");
            Console.WriteLine("✗ Filtros e transformações implementados no cliente");

            Console.WriteLine("\n=== Requisitos Não Atendidos ===");
            Console.WriteLine("• Interface uniforme para diferentes coleções");
            Console.WriteLine("• Múltiplas iterações simultâneas independentes");
            Console.WriteLine("• Iteração sem conhecer estrutura interna");
            Console.WriteLine("• Iteradores personalizados (reverso, circular, preguiçoso)");
            Console.WriteLine("• Composição de iteradores com filtros");

            // Perguntas para reflexão:
            // - Como acessar elementos sem expor representação interna?
            // - Como criar interface uniforme para diferentes coleções?
            // - Como permitir múltiplas travessias simultâneas?
            // - Como implementar diferentes formas de iteração?
        }
    }
}
