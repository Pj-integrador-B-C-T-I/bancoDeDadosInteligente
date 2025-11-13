interface ArticleCardProps {
  title: string;
  description: string;
}

export default function ArticleCard({ title, description }: ArticleCardProps) {
  return (
    <div
      className="flex flex-col w-[80vw] sm:w-[60vw] md:w-[40vw] lg:w-[26vw] xl:w-[22vw] 
                    md:h-[550px] min:h-[500px] items-center bg-[#021926] justify-start 
                    px-6 pt-6 pb-10 rounded-2xl shadow-xl shadow-black/30 
                    hover:scale-105 hover:shadow-2xl transition-transform cursor-pointer"
    >
      <div className="bg-gray-300 w-full rounded-xl overflow-hidden">
        <img
          src="/article-image-placeholder.png"
          alt="Imagem do Artigo"
          className="w-full min-h-[400px] md:h-48 lg:h-56 object-cover rounded-t-xl"
        />
      </div>

      <div className="items-center justify-center flex flex-col mt-4 text-center">
        <h2 className="text-white font-bold text-lg md:text-xl mt-2">
          {title}
        </h2>
        <p className="text-white text-sm md:text-base mt-1 px-2">
          {description}
        </p>
      </div>
    </div>
  );
}
