interface ArticleCardProps {
  title: string;
  description: string;
}

export default function ArticleCard({ title, description }: ArticleCardProps) {
  return (
    <div
      className="flex flex-col w-[70vw] sm:w-[50vw] md:w-[35vw] lg:w-[25vw] xl:w-[20vw] justify-center items-center bg-[#021926]
                    px-6 pt-6 pb-10 rounded-2xl shadow-xl shadow-black/30 
                    hover:scale-105 hover:shadow-2xl transition-transform cursor-pointer"
    >

      <div className="items-center justify-center p-6 flex flex-col mt-4 text-center">
        <h2 className="text-white font-bold text-xl md:text-2xl mt-2">
          {title}
        </h2>
        <p className="text-gray-300 text-sm md:text-base mt-1 px-2 py-4">
          {description}
        </p>
      </div>
    </div>
  );
}
