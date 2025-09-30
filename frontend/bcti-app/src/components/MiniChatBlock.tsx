import MiniChat from "./MiniChat"


export default function MiniChatBlock(){

    const messages = [
        "Explique sobre o artigo 2",
        "Explique sobre o erro 404",
        "Explique sobre o artigo 3", 
        "Explique sobre o erro 500",
    ]

    return(
        <div className="flex flex-row p-4 gap-4">
            <div className="flex flex-col justify-center gap-4">
                <MiniChat message={messages[0]} />
                <MiniChat message={messages[1]} />
            </div>
            <div className="flex flex-col justify-center gap-4">
                <MiniChat message={messages[2]} />
                <MiniChat message={messages[3]} />
            </div>


        </div>
    )
}