import Inboxes from "@/components/inboxes";
import Stars from "@/components/Stars";
import ChatLeftText from "@/components/chatLeftText";
import IconBox from "@/components/IconBox";

export default function ProductsSection() {
    return(
        <div className="w-10/12 flex flex-col justify-center px-10 rounded-2xl items-center bg-[#D9D9D9]">
            <div className="flex w-11/12 justify-between px-20 py-10">
                <div className="w-40 h-40 relative flex flex-col">

                    <IconBox icon={ChatLeftText} />

                    <div className="flex flex-col">
                        <div className="w-60">
                            <h2 className="text-3xl font-semibold mb-2">Base de Conhecimento</h2>
                        </div>

                        <div className="w-90 mt-2">
                            <p className="text-4xl font-normal mb-2 mt-4">Acesse a base de conhecimento da I.A</p>
                        </div>
                    </div>
                </div>

                <div className="w-40 relative flex flex-col">

                    <IconBox icon={Stars} />

                    <div className="flex flex-col">
                        <div className="w-60">
                        <h2 className="text-3xl font-semibold mb-2">Inteligência Artificial da CTI</h2>
                        </div>
                        <div className="w-90 mt-2">
                        <p className="text-4xl font-normal mb-2 mt-4">Torne seu trabalho mais eficiente com a I.A</p>
                        </div>
                    </div>
                </div>

                <div className="w-40 relative flex flex-col">

                    <IconBox icon={Inboxes} />

                    <div className="flex flex-col">
                        <div className="w-60">
                            <h2 className="text-3xl font-semibold mb-2">Melhore a nossa IA</h2>
                        </div>

                        <div className="w-90 mt-2">
                            <p className="text-4xl font-normal mb-2 mt-4">Adicione artigos, erros com resolução para treinarmos a I.A</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}
