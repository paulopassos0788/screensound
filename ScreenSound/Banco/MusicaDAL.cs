using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco;

internal class MusicaDAL
{
    private readonly ScreenSoundContext context;

    public MusicaDAL(ScreenSoundContext context)
    {
        this.context = context;
    }

    public IEnumerable<Musica> Listar()
    {
        return context.Musicas.ToList();
    }

    public void Adicionar(Musica musica)
    {
        context.Musicas.Add(musica);
        context.SaveChanges();
    }

    public void Atualizar(Musica musica, int id)
    {
        var musicaExistente = context.Musicas.Find(id);
        if (musicaExistente == null)
        {
            throw new ArgumentException($"Música com ID {id} não encontrada.");
        }
        musicaExistente.Nome = musica.Nome;
        context.SaveChanges();
    }

    public async Task Excluir(int id)
    {
        var musicaExistente = await context.Musicas.FindAsync(id);
        if (musicaExistente == null)
        {
            throw new ArgumentException($"Música com ID {id} não encontrada.");
        }
        context.Musicas.Remove(musicaExistente);
        await context.SaveChangesAsync();
    }

    public Musica? PesquisarMusicaPorNome(string nome)
    {
        return context.Musicas.FirstOrDefault(m => m.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }
}
