import Link from "next/link"
import MiniChat from "./MiniChat"

export default function MiniChatBlock() {
  const messages = [
    "Explique sobre o artigo 2",
    "Explique sobre o erro 404",
    "Explique sobre o artigo 3", 
    "Explique sobre o erro 500",
  ]

  return (
    <div className="w-full lg:w-1/2 flex flex-row p-4 gap-4">
      <div className="grid grid-cols-1 gap-4 p-3">
        <Link href="/Chat">
            <div className="flex gap-2">
              <MiniChat message={messages[0]} />
              <MiniChat message={messages[1]} />
              
          </div> 
          <div className="flex gap-2">
              <MiniChat message={messages[0]} />
              <MiniChat message={messages[1]} />   
          </div> 
        </Link>
        
      </div>
      
    </div>
  )
}
