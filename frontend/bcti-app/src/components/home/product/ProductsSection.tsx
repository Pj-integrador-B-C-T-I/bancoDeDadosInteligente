import ProductBlock from "./ProductBlock";


export default function ProductSection() {
  return (
    <div className="flex justify-center items-center mb-4">
        <div className="w-10/12 bg-[#D9D9D9] flex flex-col justify-center rounded-2xl items-center">
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8 w-full p-6">
                <ProductBlock
                    title="Base de Conhecimento"
                    description="Acesse a base de conhecimento da I.A"
                    icon="chat"
                />

                <ProductBlock
                    title="Inteligência Artificial da CTI"
                    description="Torne seu trabalho mais eficiente com a I.A"
                    icon="stars"
                />
                <ProductBlock
                    title="Melhore a nossa IA"
                    description="Adicione artigos, erros com resolução para treinarmos a I.A"
                    icon="inbox"
                />
            </div>
        </div>
    </div>
    
  );
}
