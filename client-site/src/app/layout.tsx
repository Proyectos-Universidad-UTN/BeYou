import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "BeYou Spa",
  description: "Tu espacio para la belleza y el bienestar",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="es">
      <body
        className={`${geistSans.variable} ${geistMono.variable} antialiased`}
      >
        {children}

        {/* Botón flotante de WhatsApp */}
        <a
          href="https://wa.me/50660744198?text=Hola%2C%20me%20gustar%C3%ADa%20agendar%20una%20cita"
          target="_blank"
          rel="noopener noreferrer"
          className="fixed bottom-6 right-6 z-50 bg-green-500 hover:bg-green-600 text-white rounded-full p-4 shadow-lg transition-all duration-300"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="28"
            height="28"
            viewBox="0 0 24 24"
            fill="currentColor"
          >
            <path d="M20.52 3.48A11.86 11.86 0 0 0 2.64 18.8L2 22l3.28-.86a11.86 11.86 0 0 0 15.24-17.66Zm-8.34 17A9.6 9.6 0 0 1 5.92 18L5.8 18l-1.94.5.5-1.88-.13-.2A9.6 9.6 0 1 1 12.18 20.5Zm5.15-7.36c-.28-.14-1.66-.82-1.92-.92s-.45-.14-.64.14-.74.92-.92 1.1-.34.21-.62.07a7.84 7.84 0 0 1-2.3-1.42 8.55 8.55 0 0 1-1.57-1.92c-.16-.28 0-.44.13-.58s.28-.34.42-.51a1.95 1.95 0 0 0 .28-.47.55.55 0 0 0 0-.51c-.07-.14-.64-1.53-.88-2.1s-.47-.48-.64-.49h-.55a1 1 0 0 0-.73.34 3 3 0 0 0-.95 2.2 5.2 5.2 0 0 0 1.1 2.68c.14.2 1.89 2.9 4.6 4a11.68 11.68 0 0 0 2.3.68c.92.09 1.76.05 2.43 0a2.31 2.31 0 0 0 1.52-1.08 1.86 1.86 0 0 0 .13-1.08c-.06-.13-.25-.2-.52-.34Z" />
          </svg>
        </a>
      </body>
    </html>
  );
}
