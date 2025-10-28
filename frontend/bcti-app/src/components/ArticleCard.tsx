export default function ArticleCard() {
    return(
    <div className="flex flex-col w-[26vw] h-200 items-center bg-[#021926] justify-center px-8 pt-8 pb-20 rounded-lg">

        <div className="bg-gray-300 h-full w-full rounded-xl">
            <img 
            src="/article-image-placeholder.png" 
            alt="Imagem do Artigo" 
            className="w-full h-40 object-cover rounded-t-lg" />
        </div>

        <div className="">
            <h2 className="text-white font-bold text-lg mt-4">Título do Artigo</h2>
            <p className="text-white text-sm mt-2">Resumo do artigo. Este é um breve resumo que descreve o conteúdo do artigo.</p>
        </div>
    </div>
    )
}