"use client";

import { useEffect } from 'react';
import { useRouter } from 'next/navigation';
import LoaderSimple from "@/components/shared/LoaderSimple";

export default function LoadingPage() {
  const router = useRouter();

  useEffect(() => {
    const timer = setTimeout(() => {
      router.push('/Home');
    }, 3000); // Redireciona após 3 segundos

    return () => clearTimeout(timer);
  }, [router]);

  return (
    <div className="flex flex-col justify-center items-center min-h-screen">
      <p className="text-base sm:text-lg mb-4 text-black">Carregando...</p>
      <LoaderSimple />
    </div>
  );
}