using System;

using DesignPatternChallenge;

namespace DesignPatternChallenge.IteratorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("    DESAFIO: SISTEMA DE PLAYLIST (ITERATOR)");
            Console.WriteLine("=================================================\n");

            // -------------------------------------------------------------
            // Código Antigo - Executando a versão sem o padrão e com problemas
            // -------------------------------------------------------------
            Console.WriteLine("------ EXECUTANDO CÓDIGO LEGADO (SEM PADRÃO) ------");
            RunLegacyCode();

            Console.WriteLine("\n\n------ EXECUTANDO CÓDIGO REFATORADO (COM ITERATOR) ------");
            RunRefactoredCode();

            Console.WriteLine("\n\n=================================================");
            Console.ReadLine();
        }

        static void RunLegacyCode()
        {
            LegacyProgram.RunLegacy();
        }

        static void RunRefactoredCode()
        {
            // O cliente só conhece as interfaces (neste caso, as fábricas baseadas nelas para geração de iteradores)
            var playlist = new RefactoredPlaylist("Nova Playlist");
            playlist.AddSong(new Song("Bohemian Rhapsody", "Queen", "Rock", 354, 1975));
            playlist.AddSong(new Song("Imagine", "John Lennon", "Pop", 183, 1971));
            playlist.AddSong(new Song("Smells Like Teen Spirit", "Nirvana", "Rock", 301, 1991));
            playlist.AddSong(new Song("Billie Jean", "Michael Jackson", "Pop", 294, 1982));
            playlist.AddSong(new Song("Hotel California", "Eagles", "Rock", 391, 1976));
            playlist.AddSong(new Song("Sweet Child O' Mine", "Guns N' Roses", "Rock", 356, 1987));

            var player = new RefactoredMusicPlayer();

            // Usando as fábricas da própria coleção que emitem Iterator:
            player.Play($"{playlist.Name} (Sequencial)", playlist.CreateIterator());
            player.Play($"{playlist.Name} (Aleatório)", playlist.CreateShuffleIterator());
            player.Play($"{playlist.Name} (Gênero: Rock)", playlist.CreateGenreIterator("Rock"));
            player.Play($"{playlist.Name} (Antigas - Lançadas antes de 2000)", playlist.CreateYearIterator(2000, true));

            // Testando a complexidade ocultada na MusicLibrary
            Console.WriteLine("\n--- Testando MusicLibrary (Complexa) ---");
            var library = new RefactoredMusicLibrary();
            library.AddSong(new Song("Song 1", "Artist A", "Pop", 200, 2020));
            library.AddSong(new Song("Song 2", "Artist B", "Rock", 300, 2021));
            // Inserimos a primeira música novamente simulando estar em chaves/artistas diferentes talvez
            library.AddSong(new Song("Song 1", "Artist A", "Pop", 200, 2020));

            // Notem como a mesma função "Play" do MusicPlayer toca qualquer iterador
            // de forma agnóstica sem saber que estava rodando sobre Dictonary<string, List<Song>> da Library!
            player.Play("Toda a Biblioteca de Música", library.CreateIterator());
        }
    }
}
