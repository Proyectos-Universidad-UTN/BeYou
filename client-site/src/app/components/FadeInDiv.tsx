// components/FadeInDiv.tsx
"use client";

import { useEffect, useState } from "react";
import { twMerge } from "tailwind-merge";

interface FadeInDivProps {
  children: React.ReactNode;
  delay?: number;
  className?: string;
}

export default function FadeInDiv({
  children,
  delay = 0,
  className,
}: FadeInDivProps) {
  const [isVisible, setIsVisible] = useState(false);

  useEffect(() => {
    // Usamos un setTimeout para controlar el retardo de la animación.
    const timer = setTimeout(() => {
      setIsVisible(true);
    }, delay);

    return () => clearTimeout(timer);
  }, [delay]);

  return (
    <div
      className={twMerge(
        "transition-opacity duration-1000",
        isVisible ? "opacity-100" : "opacity-0",
        className,
      )}
    >
      {children}
    </div>
  );
}