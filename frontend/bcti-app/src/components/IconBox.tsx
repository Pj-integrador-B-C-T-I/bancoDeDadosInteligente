import { FC } from "react";

interface IconBoxProps {
  icon?: FC; // componente React
}

export default function IconBox({ icon: Icon }: IconBoxProps) {
  return (
    <div className="w-40 h-40 flex items-end justify-center relative">
      {/* Linha vertical */}
      <div className="w-0.5 ml-2 h-full bg-black" />

      {/* Linha horizontal */}
      <div className="absolute left-0 bottom-0 w-full h-0.5 mb-2 bg-black" />

      {/* Ícone */}
      <div className="mb-2 ml-3 mr-1 pb-2">
        {Icon && <Icon />}
      </div>
    </div>
  );
}
