"use client";

import { isNil } from "lodash"
import { ArrowLeft } from "@mui/icons-material"
import { Box, Typography } from "@mui/material"
import Link from "next/link"
import { useIsMobile } from "@/hooks/UseIsMobile"

interface PageHeaderProps {
    children?: React.ReactNode
    title: string
    subtitle?: string
    backText?: string
    backPath?: string
    actionButton?: React.ReactNode
}

export const PageHeader = ({
    children,
    title,
    subtitle,
    backText,
    backPath,
    actionButton
}: PageHeaderProps) => {
    const isMobile = useIsMobile()
    return (
        <Box>
            <Box
                py={1}
                px={6}
            >
                {!isNil(backPath) && (
                    <Link href={backPath} style={{ textDecoration: 'none' }}>
                        <Box
                            display='flex'
                            flexDirection='row'
                            alignItems='center'
                            gap='1'
                            mb={4}
                            sx={{
                                color: '#5a7f7f',
                                '&:hover': {
                                    color: '#3c5c5b',
                                },
                            }}
                        >
                            <ArrowLeft sx={{ color: 'inherit' }} />
                            <Typography sx={{ color: 'inherit' }}>Regresar a {backText}</Typography>
                        </Box>
                    </Link>
                )}

                <Box
                    sx={{
                        display: 'flex',
                        width: '100%',
                        flexDirection: isMobile ? 'column' : 'row',
                        justifyContent: 'space-between',
                        alignItems: isMobile ? 'flex-start' : 'self-end',
                    }}
                >
                    <Box
                        sx={{
                            width: isMobile ? '100%' : 'auto',
                            textAlign: isMobile ? 'center' : 'left',
                        }}
                    >
                        <Typography variant="h6" >{title}</Typography>
                        <Box>
                            {!isNil(subtitle) && (
                                <Typography variant="subtitle2" >{subtitle}</Typography>
                            )}
                        </Box>
                        <Box>{children}</Box>
                    </Box>
                    {actionButton &&
                        <Box sx={{
                            width: isMobile ? '100%' : 'auto',
                            textAlign: isMobile ? 'center' : 'right',
                        }}
                        >
                            {actionButton}
                        </Box>
                    }
                </Box>
            </Box>
        </Box>
    )
}