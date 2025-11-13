"use client";

import ReactMarkdown from "react-markdown";
import remarkGfm from "remark-gfm";

interface ChatMessageProps {
  message: string;
  isQuestion?: boolean;
}

export default function ChatMessage({ message, isQuestion = false }: ChatMessageProps) {

  
  return (
    <div
      className={`flex w-full px-6 py-3 ${isQuestion ? "justify-end" : "justify-start"}`}
    >
      <div
        className={`flex items-end gap-2 max-w-[70%] ${
          isQuestion ? "flex-row-reverse" : "flex-row"
        }`}
      >
        {/* Balão da mensagem */}
        <div
          className={`px-4 py-3 rounded-2xl text-base ${
            isQuestion ? "bg-[#0F52AF] text-white" : "bg-[#5680BA] text-gray-900"
          }`}
        >
          <ReactMarkdown
            remarkPlugins={[remarkGfm]}
            components={{
              h1: ({node, ...props}) => <h1 className="text-2xl font-bold my-2" {...props} />,
              h2: ({node, ...props}) => <h2 className="text-xl font-semibold my-2" {...props} />,
              h3: ({node, ...props}) => <h3 className="text-lg font-medium my-1" {...props} />,
              strong: ({node, ...props}) => <strong className="font-bold" {...props} />,
              em: ({node, ...props}) => <em className="italic" {...props} />,
              ul: ({node, ...props}) => <ul className="list-disc ml-5 my-1" {...props} />,
              ol: ({node, ...props}) => <ol className="list-decimal ml-5 my-1" {...props} />,
              code: ({node, ...props}) => <code className="bg-gray-200 text-sm px-1 py-0.5 rounded" {...props} />,
              pre: ({node, ...props}) => <pre className="bg-gray-200 p-2 rounded overflow-x-auto" {...props} />,
              p: ({node, ...props}) => <p className="my-1" {...props} />
            }}
          >
            {message.replace(/\\n/g, "\n")}
          </ReactMarkdown>


        </div>
      </div>
    </div>
  );
}
