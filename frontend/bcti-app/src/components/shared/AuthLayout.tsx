import { purplePurse } from "@/app/fonts";

interface AuthLayoutProps {
  children: React.ReactNode;
  text?: string; 
}

export default function AuthLayout({ children}: AuthLayoutProps) {
  return (
    <div className="flex h-screen w-screen items-center justify-start">
      {/* Lado esquerdo com logo */}
      <div className="w-1/2 h-screen bg-[#031926] flex items-center justify-center">
        <h1
          className={`${purplePurse.className} text-white text-9xl font-bold m-10`}
        >
          CT.IA
        </h1>
      </div>

      {/* Lado direito com conteúdo injetado */}
      <div className="w-1/2 h-screen flex items-center justify-center">
        {children}
      </div>
    </div>
  );
}
