import { ArrowRight } from "lucide-react";

interface MiniChatProps {
  message?: string; 
}

export default function MiniChat({ message = "Explique sobre o artigo 2" }: MiniChatProps){
    return(
        <div className="w-xs h-50 rounded-2xl bg-[#D9D9D9]">
                <div className="w-full h-full flex items-end justify-end flex-col gap-4 p-6" >
                    <div className="bg-[#0F52AF] w-4/5 rounded-2xl h-15 flex justify-end px-4 items-center" >
                        <div className="w-30">
                            <p className="text-black font-semibold text-right text-lg py-2">Bem vindo ao CT.IA</p>
                        </div>
                    </div>
                    <div className="flex justify-start w-full gap-2 items-center">
                        <div className="bg-[#5680BA] w-4/5 rounded-2xl h-15 flex justify-end px-4 items-center" >
                            <div className="w-auto">
                                <p className="text-black font-semibold text-right text-lg py-2">{message}</p>
                            </div>
                        </div>
                        <div className="flex items-center h-10 w-10 p-2 justify-center bg-black rounded-full">
                            <ArrowRight className="h-10 w-10 text-white"  />
                        </div>

                    </div>
                    
                </div>

            </div>
    )
}