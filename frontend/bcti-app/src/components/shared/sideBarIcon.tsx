"use client"

import { PanelLeft } from "lucide-react"
import { Button } from "@/components/ui/button"

export function SidebarIcon() {
  return (
    <Button className="cursor-pointer" size="icon" >
      {/* Troque Menu por PanelLeft se quiser mais parecido */}
      <PanelLeft className="h-10 w-10 text-white" />
    </Button>
  )
}
