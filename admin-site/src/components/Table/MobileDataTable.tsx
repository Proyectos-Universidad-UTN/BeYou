"use client"

import React, { useState } from "react"
import { GridColDef, GridRowId } from "@mui/x-data-grid"
import { Card, CardContent, Checkbox } from "@mui/material"

interface MobileDataTableProps {
    columns: GridColDef[]
    rows: any[]
    onRowClick?: (params: any) => void
    checkboxSelection?: boolean
    rowSelectionModel?: GridRowId[]
    onRowSelectionModelChange?: (newSelection: GridRowId[]) => void
}

export const MobileDataTable = ({
    columns,
    rows,
    onRowClick,
    checkboxSelection = false,
    rowSelectionModel = [],
    onRowSelectionModelChange
}: MobileDataTableProps) => {
    const [localSelection, setLocalSelection] = useState<GridRowId[]>([])

    const currentSelection = rowSelectionModel?.length ? rowSelectionModel : localSelection

    const handleCheckboxChange = (id: GridRowId) => {
        const newSelection = currentSelection.includes(id)
            ? currentSelection.filter((selectedId) => selectedId !== id)
            : [...currentSelection, id]

        if (onRowSelectionModelChange) {
            onRowSelectionModelChange(newSelection)
        } else {
            setLocalSelection(newSelection)
        }
    }

    return (
        <div className="flex flex-col gap-4">
            {rows.map((row, index) => (
                <Card
                    key={row.id ?? index}
                    className="bg-gray-100 shadow-sm rounded-2xl cursor-pointer"
                    onClick={() => onRowClick?.({ row })}
                >
                    <CardContent className="p-4">
                        {checkboxSelection && (
                            <div className="flex justify-end mb-2">
                                <Checkbox
                                    checked={currentSelection.includes(row.id)}
                                    onChange={() => handleCheckboxChange(row.id)}
                                    onClick={(e) => e.stopPropagation()}
                                />
                            </div>
                        )}

                        <div className="grid gap-2">
                            {columns.map((col) => {
                                const value = col.field.includes(".")
                                    ? col.field.split(".").reduce((acc, part) => acc?.[part], row)
                                    : row[col.field]

                                return (
                                    <div key={col.field} className="text-sm">
                                        <span className="font-bold text-gray-600">{col.headerName ?? col.field}:</span>{" "}
                                        <span className="text-gray-800">{value ?? "No disponible"}</span>
                                    </div>
                                )
                            })}
                        </div>
                    </CardContent>
                </Card>
            ))}
        </div>
    )
}
