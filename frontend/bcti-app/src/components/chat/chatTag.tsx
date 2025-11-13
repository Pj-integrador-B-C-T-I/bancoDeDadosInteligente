import Link from "next/link";

interface ChatTagProps{
    content?: string;
    current?: boolean;
    href: string;
}

export default function ChatTag({content = "Novo Chat", current = false, href}: ChatTagProps){
    return(
        <>  
            <Link href={href}>
                <div 
                    className={`w-full mt-1 pr-6 py-3 flex items-center justify-end rounded-2xl transition-transform duration-200 hover:scale-105 cursor-pointer ${current ? "bg-[#5680BA] scale-105" : "bg-[#052450]"}`} 
                >
                    <p 
                        className={`${current? "text-white" : "text-white/80"} font-semibold text-right text-md`}
                    >
                        {content}
                    </p>
                </div>
            </Link>
            
        </>
        

    )
}