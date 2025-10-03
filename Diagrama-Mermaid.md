`````mermaid
classDiagram
    class Usuario {
        -id: int
        -nome: string
        -email: string
        -senha: string
        +login(email, senha)
        +logout()
    }

    class TecnicoSuporte {
        +buscarSolucao(palavraChave: string)
    }

    class AnalistaSistemas {
        +cadastrarArtigo(artigo: Artigo)
    }

    class GerenteProjeto {
        +visualizarRelatorios(): Relatorio
    }

    class Artigo {
        -id: int
        -titulo: string
        -descricao: string
        -categoria: string
        -anexos: File[]
        +editar()
        +visualizar()
    }

    class Relatorio {
        -id: int
        -periodo: string
        -dados: string
        +gerar()
        +exibir()
    }

    class ChatInteligente {
        +responderPergunta(pergunta: string): string
        +recomendarArtigos(historico: Usuario): Artigo[]
    }

    class BuscaInteligente {
        +buscar(palavraChave: string): Artigo[]
        +ordenarPorRelevancia()
    }

    class CategorizacaoAutomatica {
        +categorizar(artigo: Artigo): string
    }

    class Sistema {
        +autenticarUsuario(email: string, senha: string): Usuario
        +registrarArtigo(artigo: Artigo)
        +executarBusca(palavraChave: string)
    }

    Usuario <|-- TecnicoSuporte
    Usuario <|-- AnalistaSistemas
    Usuario <|-- GerenteProjeto

    Sistema --> Usuario
    Sistema --> Artigo
    Sistema --> Relatorio
    Sistema --> ChatInteligente
    Sistema --> BuscaInteligente
    Sistema --> CategorizacaoAutomatica

    AnalistaSistemas --> Artigo
    TecnicoSuporte --> BuscaInteligente
    TecnicoSuporte --> ChatInteligente
    GerenteProjeto --> Relatorio
    CategorizacaoAutomatica --> Artigo
    BuscaInteligente --> Artigo
    ChatInteligente --> Artigo
