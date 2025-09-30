"use client"

import { purplePurse } from "@/app/fonts";
import { SidebarIcon } from "./shared/sideBarIcon";


export default function Navbar() {
  return (
    <div className="w-screen h-20 flex flex-row items-center bg-[#031926] justify-between px-4">
      <SidebarIcon />
      <h1 className={`${purplePurse.className} text-white text-4xl font-bold m-10`}>CT.IA</h1>
      <div className="rounded-full h-10 w-10 bg-gray-500" />
    </div>
  )
}
