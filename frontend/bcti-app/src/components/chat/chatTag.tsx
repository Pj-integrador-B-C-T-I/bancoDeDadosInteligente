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
                    className={`w-full px-6 py-3 flex items-center justify-end rounded-2xl transition-transform duration-200 hover:scale-105 cursor-pointer ${current ? "bg-[#5680BA] scale-105" : "bg-[#125988]"}`} 
                >
                    <p className="text-white font-semibold text-right text-xl">{content}</p>
                </div>
            </Link>
            
        </>
        

    )
}