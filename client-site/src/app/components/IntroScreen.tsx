"use client";

import React from "react";

export default function IntroScreen() {
  return (
    <div className="flex h-screen w-full flex-col items-center justify-center bg-gradient-to-b from-[#FCEEF5] via-[#F8D6EB] to-[#F3B6DA] transition-opacity duration-1000">
      <div className="flex flex-col items-center">
        {/* Logo animado estilizado */}
        <div className="relative mb-6 animate-pulse">
          <div className="h-24 w-24 rounded-full bg-[#D291BC] shadow-lg"></div>
          <div className="absolute left-1/2 top-1/2 h-14 w-14 -translate-x-1/2 -translate-y-1/2 rounded-full bg-[#FCEEF5] animate-ping"></div>
          <div className="absolute left-1/2 top-1/2 flex h-14 w-14 -translate-x-1/2 -translate-y-1/2 items-center justify-center rounded-full bg-[#FCEEF5]">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="28"
              height="28"
              viewBox="0 0 24 24"
              fill="none"
              stroke="#D291BC"
              strokeWidth="2"
              strokeLinecap="round"
              strokeLinejoin="round"
              className="lucide lucide-sparkles h-8 w-8"
            >
              <path d="M12 2v2" />
              <path d="M12 20v2" />
              <path d="M4.93 4.93l1.41 1.41" />
              <path d="M17.66 17.66l1.41 1.41" />
              <path d="M2 12h2" />
              <path d="M20 12h2" />
              <path d="M4.93 19.07l1.41-1.41" />
              <path d="M17.66 6.34l1.41-1.41" />
              <circle cx="12" cy="12" r="5" />
            </svg>
          </div>
        </div>

        {/* Mensaje principal */}
        <h1 className="animate-fade-in px-4 text-center font-serif text-3xl font-semibold text-[#523249] md:px-8 md:text-5xl">
         ✨ Donde cada detalle refleja tu belleza ✨
        </h1>

        <p className="mt-4 animate-fade-in text-center text-lg text-[#7A4E74] md:text-xl">
          Uñas, estilo y cuidado personal en un solo lugar
        </p>
      </div>
    </div>
  );
}
