"use client";

import {
  X,
  User,
  Home,
  LogOut,
  BarChart3,
  MessageCircle,
  ChevronDown,
  ChevronUp,
} from "lucide-react";
import { purplePurse } from "@/app/fonts";
import { Fade as Hamburger } from "hamburger-react";
import { useState, useEffect } from "react";
import Link from "next/link";
import { motion, AnimatePresence } from "framer-motion";
import {jwtDecode} from "jwt-decode";

interface UserData {
  id: number;
  nome: string;
  email: string;
}

interface JwtPayload {
  nameid: string; // ClaimTypes.NameIdentifier
  unique_name?: string; // ClaimTypes.Name
  email: string; // ClaimTypes.Email
  role: string; // ClaimTypes.Role
  exp: number;
}


export default function Navbar() {
  const [isOpen, setOpen] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [userData, setUserData] = useState<UserData | null>(null);
  const [loading, setLoading] = useState(true);
  const [relatoriosOpen, setRelatoriosOpen] = useState(false);

  const getUserFromToken = (token: string): UserData | null => {
    try {
      const decoded: JwtPayload = jwtDecode(token);

      return {
        id: parseInt(decoded.nameid),
        nome: decoded.unique_name || "",
        email: decoded.email,
      };
    } catch (err) {
      console.error("Erro ao decodificar token:", err);
      return null;
    }
  };

  

  useEffect(() => {
    const token = localStorage.getItem("token");

    if (token) {
      setIsLoggedIn(true);

      // Tenta recuperar usuário do localStorage
      const userStr = localStorage.getItem("user");
      if (userStr) {
        try {
          setUserData(JSON.parse(userStr));
        } catch (err) {
          console.error("Erro ao parsear user do localStorage:", err);
        }
      } else {
        // Se não tiver no localStorage, decodifica direto do token
        const user = getUserFromToken(token);
        if (user) {
          setUserData(user);
          localStorage.setItem("user", JSON.stringify(user));
        }
      }
    } else {
      setIsLoggedIn(false);
      setUserData(null);
    }

    setLoading(false);
  }, []);


  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    localStorage.removeItem("userId");
    setIsLoggedIn(false);
    setUserData(null);
    setOpen(false);
    window.location.href = "/";
  };

  const menuItems = [
    { icon: Home, label: "Início", href: "/" },
    { icon: User, label: "Minha Conta", href: "/MeusDados", requiresAuth: true },
    {
      icon: BarChart3,
      label: "Relatórios",
      dropdown: [
        { label: "Visualizar Relatórios", href: "/Relatorios" },
      ],
    },
    { icon: MessageCircle, label: "Chat", href: "/Chat", requiresAuth: true },
  ];

  return (
    <div>
      {/* Navbar */}
      <div className="w-screen bg-[#031926] py-5 px-9 flex flex-row justify-between items-center gap-4">
        {/* Botão hambúrguer */}
        <Hamburger toggled={isOpen} toggle={setOpen} color="#FFF" rounded />

        {/* Logo */}
        <Link href="/">
          <h1 className={`${purplePurse.className} text-white text-4xl font-bold`}>
            CT.IA
          </h1>
        </Link>
       
        
      </div>

      {/* Menu Overlay */}
      <AnimatePresence>
        {isOpen && (
          <>
            {/* Backdrop */}
            <motion.div
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              exit={{ opacity: 0 }}
              className="fixed inset-0 bg-black bg-opacity-50 z-40"
              onClick={() => setOpen(false)}
            />

            {/* Menu lateral */}
            <motion.div
              initial={{ x: -300, opacity: 0 }}
              animate={{ x: 0, opacity: 1 }}
              exit={{ x: -300, opacity: 0 }}
              transition={{ type: "spring", damping: 25, stiffness: 200 }}
              className="fixed top-0 left-0 h-full w-80 bg-white shadow-xl z-50"
            >
              {/* Cabeçalho do Menu */}
              <div className="p-6 border-b border-gray-200">
                <div className="flex items-center justify-between mb-6">
                  <p className="text-gray-800 text-2xl font-bold">Menu</p>
                  <button
                    onClick={() => setOpen(false)}
                    className="p-2 rounded-full hover:bg-gray-100 transition-colors"
                  >
                    <X size={20} />
                  </button>
                </div>

                {isLoggedIn ? (
                  <div className="flex items-center gap-3">
                    <div className="w-10 h-10 rounded-full bg-gray-300 flex items-center justify-center">
                      <User size={20} className="text-gray-600" />
                    </div>
                    <div>
                      {loading ? (
                        <>
                          <div className="h-4 bg-gray-200 rounded w-24 mb-2 animate-pulse"></div>
                          <div className="h-3 bg-gray-200 rounded w-16 animate-pulse"></div>
                        </>
                      ) : (
                        <>
                          <p className="font-medium text-gray-800">
                            Olá, {userData?.nome || "Usuário"}
                          </p>
                          <p className="text-sm text-gray-500">
                            Bem-vindo de volta
                          </p>
                        </>
                      )}
                    </div>
                  </div>
                ) : (
                  <div className="flex gap-3">
                    <Link
                      href="/Login"
                      className="flex-1 bg-gray-800 text-white py-2 px-4 rounded-lg text-center hover:bg-gray-900 transition-colors"
                      onClick={() => setOpen(false)}
                    >
                      Entrar
                    </Link>
                    <Link
                      href="/Cadastro"
                      className="flex-1 border border-gray-300 text-gray-700 py-2 px-4 rounded-lg text-center hover:bg-gray-50 transition-colors"
                      onClick={() => setOpen(false)}
                    >
                      Cadastrar
                    </Link>
                  </div>
                )}
              </div>

              {/* Itens do Menu */}
              <nav className="p-4">
                <ul className="space-y-2">
                  {menuItems.map((item, index) => {
                    if (item.requiresAuth && !isLoggedIn) return null;

                    const IconComponent = item.icon;

                    // Caso seja dropdown (Relatórios)
                    if (item.dropdown) {
                      return (
                        <li key={index}>
                          <button
                            onClick={() => setRelatoriosOpen(!relatoriosOpen)}
                            className="flex items-center justify-between w-full p-3 rounded-lg text-gray-700 hover:bg-gray-100 transition-colors"
                          >
                            <span className="flex items-center gap-3">
                              <IconComponent size={20} />
                              <span className="font-medium">{item.label}</span>
                            </span>
                            {relatoriosOpen ? (
                              <ChevronUp size={18} />
                            ) : (
                              <ChevronDown size={18} />
                            )}
                          </button>

                          <AnimatePresence>
                            {relatoriosOpen && (
                              <motion.ul
                                initial={{ height: 0, opacity: 0 }}
                                animate={{ height: "auto", opacity: 1 }}
                                exit={{ height: 0, opacity: 0 }}
                                className="ml-8 mt-2 space-y-2 overflow-hidden"
                              >
                                {item.dropdown.map((sub, subIndex) => (
                                  <li key={subIndex}>
                                    <Link
                                      href={sub.href}
                                      className="block p-2 text-gray-600 rounded-lg hover:bg-gray-100 transition-colors"
                                      onClick={() => setOpen(false)}
                                    >
                                      {sub.label}
                                    </Link>
                                  </li>
                                ))}
                              </motion.ul>
                            )}
                          </AnimatePresence>
                        </li>
                      );
                    }

                    // Caso seja item normal
                    return (
                      <li key={index}>
                        <Link
                          href={item.href}
                          className="flex items-center gap-3 p-3 rounded-lg text-gray-700 hover:bg-gray-100 transition-colors"
                          onClick={() => setOpen(false)}
                        >
                          <IconComponent size={20} />
                          <span className="font-medium">{item.label}</span>
                        </Link>
                      </li>
                    );
                  })}
                </ul>
              </nav>

              {/* Rodapé do Menu */}
              {isLoggedIn && (
                <div className="absolute bottom-0 left-0 right-0 p-4 border-t border-gray-200">
                  <button
                    onClick={handleLogout}
                    className="flex items-center cursor-pointer gap-3 w-full p-3 rounded-lg text-gray-700 hover:bg-gray-100 transition-colors"
                  >
                    <LogOut size={20} />
                    <span className="font-medium">Sair</span>
                  </button>
                </div>
              )}
            </motion.div>
          </>
        )}
      </AnimatePresence>
    </div>
  );
}
