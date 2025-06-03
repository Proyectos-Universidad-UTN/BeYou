USE BeYou
GO

-- Genders
SET IDENTITY_INSERT Gender ON;

INSERT INTO Gender
(Id, [Name])
VALUES
(1, 'Masculino'),
(2, 'Femenino'),
(3, 'Otro')

SET IDENTITY_INSERT Gender OFF;

-- Role

SET IDENTITY_INSERT [Role] ON;

INSERT INTO [Role]
(Id, [Description], [Type], Created, CreatedBy)
VALUES
(1, 'Acceso completo', 'Administrador', GETDATE(), 'admin')

SET IDENTITY_INSERT [Role] OFF;

-- Tax

SET IDENTITY_INSERT [Tax] ON;

INSERT INTO [Tax]
(Id, [Name], Rate, Created, CreatedBy, Active)
VALUES
(1, 'IVA general 13%', 13, GETDATE(), 'admin', 1)

SET IDENTITY_INSERT [Tax] OFF;

-- Province

SET IDENTITY_INSERT Province ON;

INSERT INTO Province (Id, [Name])
VALUES
(1, 'San José'),
(2, 'Alajuela'),
(3, 'Cartago'),
(4, 'Heredia'),
(5, 'Guanacaste'),
(6, 'Puntarenas'),
(7, 'Limón');

SET IDENTITY_INSERT Province OFF;

-- Canton

SET IDENTITY_INSERT Canton ON;

-- San José (ProvinceId = 1)
INSERT INTO Canton (Id, ProvinceId, [Name]) VALUES
(1, 1, 'San José'),
(2, 1, 'Escazú'),
(3, 1, 'Desamparados'),
(4, 1, 'Puriscal'),
(5, 1, 'Tarrazú'),
(6, 1, 'Aserrí'),
(7, 1, 'Mora'),
(8, 1, 'Goicoechea'),
(9, 1, 'Santa Ana'),
(10, 1, 'Alajuelita'),
(11, 1, 'Vásquez de Coronado'),
(12, 1, 'Acosta'),
(13, 1, 'Tibás'),
(14, 1, 'Moravia'),
(15, 1, 'Montes de Oca'),
(16, 1, 'Turrubares'),
(17, 1, 'Dota'),
(18, 1, 'Curridabat'),
(19, 1, 'Pérez Zeledón'),
(20, 1, 'León Cortés');

-- Alajuela (ProvinceId = 2)
INSERT INTO Canton (Id, ProvinceId, [Name]) VALUES
(21, 2, 'Alajuela'),
(22, 2, 'San Ramón'),
(23, 2, 'Grecia'),
(24, 2, 'San Mateo'),
(25, 2, 'Atenas'),
(26, 2, 'Naranjo'),
(27, 2, 'Palmares'),
(28, 2, 'Poás'),
(29, 2, 'Orotina'),
(30, 2, 'San Carlos'),
(31, 2, 'Zarcero'),
(32, 2, 'Sarchí'),
(33, 2, 'Upala'),
(34, 2, 'Los Chiles'),
(35, 2, 'Guatuso'),
(36, 2, 'Río Cuarto');

-- Cartago (ProvinceId = 3)
INSERT INTO Canton (Id, ProvinceId, [Name]) VALUES
(37, 3, 'Cartago'),
(38, 3, 'Paraíso'),
(39, 3, 'La Unión'),
(40, 3, 'Jiménez'),
(41, 3, 'Turrialba'),
(42, 3, 'Alvarado'),
(43, 3, 'Oreamuno'),
(44, 3, 'El Guarco');

-- Heredia (ProvinceId = 4)
INSERT INTO Canton (Id, ProvinceId, [Name]) VALUES
(45, 4, 'Heredia'),
(46, 4, 'Barva'),
(47, 4, 'Santo Domingo'),
(48, 4, 'Santa Bárbara'),
(49, 4, 'San Rafael'),
(50, 4, 'San Isidro'),
(51, 4, 'Belén'),
(52, 4, 'Flores'),
(53, 4, 'San Pablo'),
(54, 4, 'Sarapiquí');

-- Guanacaste (ProvinceId = 5)
INSERT INTO Canton (Id, ProvinceId, [Name]) VALUES
(55, 5, 'Liberia'),
(56, 5, 'Nicoya'),
(57, 5, 'Santa Cruz'),
(58, 5, 'Bagaces'),
(59, 5, 'Carrillo'),
(60, 5, 'Cañas'),
(61, 5, 'Abangares'),
(62, 5, 'Tilarán'),
(63, 5, 'Nandayure'),
(64, 5, 'La Cruz'),
(65, 5, 'Hojancha');

-- Puntarenas (ProvinceId = 6)
INSERT INTO Canton (Id, ProvinceId, [Name]) VALUES
(66, 6, 'Puntarenas'),
(67, 6, 'Esparza'),
(68, 6, 'Buenos Aires'),
(69, 6, 'Montes de Oro'),
(70, 6, 'Osa'),
(71, 6, 'Quepos'),
(72, 6, 'Golfito'),
(73, 6, 'Coto Brus'),
(74, 6, 'Parrita'),
(75, 6, 'Corredores'),
(76, 6, 'Garabito'),
(77, 6, 'Monteverder');

-- Limón (ProvinceId = 7)
INSERT INTO Canton (Id, ProvinceId, [Name]) VALUES
(78, 7, 'Limón'),
(79, 7, 'Pococí'),
(80, 7, 'Siquirres'),
(81, 7, 'Talamanca'),
(82, 7, 'Matina'),
(83, 7, 'Guácimo');

SET IDENTITY_INSERT Canton OFF;

-- District

SET IDENTITY_INSERT District ON;

-- San josé
INSERT INTO District (Id, CantonId, Name) VALUES
(1, 1, N'Carmen'),
(2, 1, N'Merced'),
(3, 1, N'Hospital'),
(4, 1, N'Catedral'),
(5, 1, N'Zapote'),
(6, 1, N'San Francisco de Dos Ríos'),
(7, 1, N'Uruca'),
(8, 1, N'Mata Redonda'),
(9, 1, N'Pavas'),
(10, 1, N'Hatillo'),
(11, 1, N'San Sebastián'),

(12, 2, N'Escazú'),
(13, 2, N'San Antonio'),
(14, 2, N'San Rafael'),

(15, 3, N'Desamparados'),
(16, 3, N'San Miguel'),
(17, 3, N'San Juan de Dios'),
(18, 3, N'San Rafael Arriba'),
(19, 3, N'San Antonio'),
(20, 3, N'Frailes'),
(21, 3, N'Patarra'),
(22, 3, N'San Cristobal'),
(23, 3, N'Rosario'),
(24, 3, N'Damas'),
(25, 3, N'San Rafael Abajo'),
(26, 3, N'Gravilias'),
(27, 3, N'Los Guido'),

(28, 4, N'Santiago'),
(29, 4, N'Mercedes Sur'),
(30, 4, N'Barbacoas'),
(31, 4, N'Grifo Alto'),
(32, 4, N'San Rafael'),
(33, 4, N'Candelarita'),
(34, 4, N'Desamparaditos'),
(35, 4, N'San Antonio'),
(36, 4, N'Chires'),

(37, 5, N'San Marcos'),
(38, 5, N'San Lorenzo'),
(39, 5, N'San Carlos'),

(40, 6, N'Aserrí'),
(41, 6, N'Tarbaca'),
(42, 6, N'Vuelta de Jorco'),
(43, 6, N'San Gabriel'),
(44, 6, N'Legua'),
(45, 6, N'Monterrey'),
(46, 6, N'Salitrillos'),

(47, 7, N'Colón'),
(48, 7, N'Guayabo'),
(49, 7, N'Tabarcia'),
(50, 7, N'Piedras Negras'),
(51, 7, N'Picagres'),
(52, 7, N'Jaris'),
(53, 7, N'Quitirrisí'),

(54, 8, N'Guadalupe'),
(55, 8, N'San Francisco'),
(56, 8, N'Calle Blancos'),
(57, 8, N'Mata de Plátano'),
(58, 8, N'Ipis'),
(59, 8, N'Rancho Redondo'),
(60, 8, N'Purral'),

(61, 9, N'Santa Ana'),
(62, 9, N'Salitral'),
(63, 9, N'Pozos'),
(64, 9, N'Uruca'),
(65, 9, N'Piedades'),
(66, 9, N'Brasil'),

(67, 10, N'Alajuelita'),
(68, 10, N'San Josecito'),
(69, 10, N'San Antonio'),
(70, 10, N'Concepción'),
(71, 10, N'San Felipe'),

(72, 11, N'San Isidro'),
(73, 11, N'San Rafael'),
(74, 11, N'Dulce Nombre de Jesús'),
(75, 11, N'Patalillo'),
(76, 11, N'Cascajal'),

(77, 12, N'San Ignacio'),
(78, 12, N'Guaitil'),
(79, 12, N'Palmical'),
(80, 12, N'Cangrejal'),
(81, 12, N'Sabanillas'),

(82, 13, N'San Juan'),
(83, 13, N'Cinco Esquinas'),
(84, 13, N'Anselmo Llorrente'),
(85, 13, N'León XIII'),
(86, 13, N'Colima'),

(87, 14, N'San Vicente'),
(88, 14, N'San Jerónimo'),
(89, 14, N'La Trinidad'),

(90, 15, N'San Pedro'),
(91, 15, N'Sabanilla'),
(92, 15, N'Mercedes'),
(93, 15, N'San Rafael'),

(94, 16, N'San Pablo'),
(95, 16, N'San Pedro'),
(96, 16, N'San Juan de Mata'),
(97, 16, N'San Luis'),
(98, 16, N'Carara'),

(99, 17, N'Santa María'),
(100, 17, N'Jardín'),
(101, 17, N'Copey'),

(102, 18, N'Curridabat'),
(103, 18, N'Granadilla'),
(104, 18, N'Sanchéz'),
(105, 18, N'Tirrases'),

(106, 19, N'San Isidro de El General'),
(107, 19, N'El General'),
(108, 19, N'Daniel Flores'),
(109, 19, N'Rivas'),
(110, 19, N'San Pedro'),
(111, 19, N'Platanares'),
(112, 19, N'Pejibaye'),
(113, 19, N'Cajón'),
(114, 19, N'Barú'),
(115, 19, N'Río Nuevo'),
(116, 19, N'Paramo'),
(117, 19, N'La Amistad'),

(118, 20, N'San Pablo'),
(119, 20, N'San Andrés'),
(120, 20, N'Llano Bonito'),
(121, 20, N'San Isidro'),
(122, 20, N'Santa Cruz'),
(123, 20, N'San Antonio'),

-- Alajuela
(124, 21, 'Alajuela'),
(125, 21, 'San José'),
(126, 21, 'Carrizal'),
(127, 21, 'San Antonio'),
(128, 21, 'Guácima'),
(129, 21, 'San Isidro'),
(130, 21, 'Sabanilla'),
(131, 21, 'San Rafael'),
(132, 21, 'Río Segundo'),
(133, 21, 'Desamparados'),
(134, 21, 'Turrucares'),
(135, 21, 'Tambor'),
(136, 21, 'Garita'),
(137, 21, 'Sarapiquí'),

(138, 22, 'San Ramón'),
(139, 22, 'Santiago'),
(140, 22, 'San Juan'),
(141, 22, 'Piedades Norte'),
(142, 22, 'Piedades Sur'),
(143, 22, 'San Rafael'),
(144, 22, 'San Isidro'),
(145, 22, 'Ángeles'),
(146, 22, 'Alfaro'),
(147, 22, 'Volio'),
(148, 22, 'Concepción'),
(149, 22, 'Zapotal'),
(150, 22, 'Peñas Blancas'),
(151, 22, 'San Lorenzo'),

(152, 23, 'Grecia'),
(153, 23, 'San Isidro'),
(154, 23, 'San José'),
(155, 23, 'San Roque'),
(156, 23, 'Tacares'),
(157, 23, 'Puente de Piedra'),
(158, 23, 'Bolivar'),

(159, 24, 'San Mateo'),
(160, 24, 'Desmonte'),
(161, 24, 'Jesús María'),
(162, 24, 'Labrador'),

(163, 25, 'Atenas'),
(164, 25, 'Jesús'),
(165, 25, 'Mercedes'),
(166, 25, 'San Isidro'),
(167, 25, 'Concepción'),
(168, 25, 'San José'),
(169, 25, 'Santa Eulalia'),
(170, 25, 'Escobal'),

(171, 26, 'Naranjo'),
(172, 26, 'San Miguel'),
(173, 26, 'San José'),
(174, 26, 'Cirrí Sur'),
(175, 26, 'San Jerónimo'),
(176, 26, 'San Juan'),
(177, 26, 'El Rosario'),
(178, 26, 'Palmitos'),

(179, 27, 'Palmares'),
(180, 27, 'Zaragoza'),
(181, 27, 'Buenos Aires'),
(182, 27, 'Santiago'),
(183, 27, 'Candelaria'),
(184, 27, 'Esquipulas'),
(185, 27, 'La Granja'),

(186, 28, 'San Pedro'),
(187, 28, 'San Juan'),
(188, 28, 'San Rafael'),
(189, 28, 'Carrillos'),
(190, 28, 'Sabana Redonda'),

(191, 29, 'Orotina'),
(192, 29, 'El Mastate'),
(193, 29, 'Hacienda Vieja'),
(194, 29, 'Coyolar'),
(195, 29, 'La Ceiba'),

(196, 30, 'Quesada'),
(197, 30, 'Florencia'),
(198, 30, 'Buenavista'),
(199, 30, 'Aguas Zarcas'),
(200, 30, 'Venecia'),
(201, 30, 'Pital'),
(202, 30, 'La Fortuna'),
(203, 30, 'La Tigra'),
(204, 30, 'La Palmera'),
(205, 30, 'Venado'),
(206, 30, 'Cutris'),
(207, 30, 'Monterrey'),
(208, 30, 'Pocosol'),

(209, 31, 'Zarcero'),
(210, 31, 'Laguna'),
(211, 31, 'Tapesco'),
(212, 31, 'Guadalupe'),
(213, 31, 'Palmira'),
(214, 31, 'Zapote'),
(215, 31, 'Brisas'),

(216, 32, 'Sarchí Norte'),
(217, 32, 'Sarchí Sur'),
(218, 32, 'Toro Amarillo'),
(219, 32, 'San Pedro'),
(220, 32, 'Rodríguez'),

(221, 33, 'Upala'),
(222, 33, 'Aguas Claras'),
(223, 33, 'San José O Pizote'),
(224, 33, 'Bijagua'),
(225, 33, 'Delicias'),
(226, 33, 'Dos Ríos'),
(227, 33, 'Yolillal'),
(228, 33, 'Canalete'),

(229, 34, 'Los Chiles'),
(230, 34, 'Caño Negro'),
(231, 34, 'El Amparo'),
(232, 34, 'San Jorge'),

(233, 35, 'San Rafael'),
(234, 35, 'Buenavista'),
(235, 35, 'Cote'),
(236, 35, 'Katira'),

(237, 36, 'Río Cuarto'),
(238, 36, 'Santa Rita'),
(239, 36, 'Santa Isabel'),

-- Cartago

(240, 37, 'Oriental'),
(241, 37, 'Occidental'),
(242, 37, 'Carmen'),
(243, 37, 'San Nicolás'),
(244, 37, 'Aguacaliente o San Francisco'),
(245, 37, 'Guadalupe o Arenilla'),
(246, 37, 'Corralillo'),
(247, 37, 'Tierra Blanca'),
(248, 37, 'Dulce Nombre'),
(249, 37, 'Llano Grande'),
(250, 37, 'Quebradilla'),

(251, 38, 'Paraíso'),
(252, 38, 'Santiago'),
(253, 38, 'Orosi'),
(254, 38, 'Cachí'),
(255, 38, 'Llanos de Santa Lucía'),
(256, 38, 'Birrisito'),

(257, 39, 'Tres Ríos'),
(258, 39, 'San Diego'),
(259, 39, 'San Juan'),
(260, 39, 'San Rafael'),
(261, 39, 'Concepción'),
(262, 39, 'Dulce Nombre'),
(263, 39, 'San Ramón'),
(264, 39, 'Río Azul'),

(265, 40, 'Juan Viñas'),
(266, 40, 'Tucurrique'),
(267, 40, 'Pejibaye'),

(268, 41, 'Turrialba'),
(269, 41, 'La Suiza'),
(270, 41, 'Peralta'),
(271, 41, 'Santa Cruz'),
(272, 41, 'Santa Teresita'),
(273, 41, 'Pavones'),
(274, 41, 'Tuis'),
(275, 41, 'Tayutic'),
(276, 41, 'Santa Rosa'),
(277, 41, 'Tres Equis'),
(278, 41, 'La Isabel'),
(279, 41, 'Chirripó'),

(280, 42, 'Pacayas'),
(281, 42, 'Cervantes'),
(282, 42, 'Capellades'),

(283, 43, 'San Rafael'),
(284, 43, 'Cot'),
(285, 43, 'Potrero Cerrado'),
(286, 43, 'Cipreses'),
(287, 43, 'Santa Rosa'),

(288, 44, 'El Tejar'),
(289, 44, 'San Isidro'),
(290, 44, 'Tobosi'),
(291, 44, 'Patio de Agua'),

-- Heredia

(292, 45, 'Heredia'),
(293, 45, 'Mercedes'),
(294, 45, 'San Francisco'),
(295, 45, 'Ulloa'),
(296, 45, 'Varablanca'),

(297, 46, 'Barva'),
(298, 46, 'San Pedro'),
(299, 46, 'San Pablo'),
(300, 46, 'San Roque'),
(301, 46, 'Santa Lucía'),
(302, 46, 'San José de la Montaña'),

(303, 47, 'Santo Domingo'),
(304, 47, 'San Vicente'),
(305, 47, 'San Miguel'),
(306, 47, 'Paracito'),
(307, 47, 'Santo Tomás'),
(308, 47, 'Santa Rosa'),
(309, 47, 'Tures'),
(310, 47, 'Pará'),

(311, 48, 'Santa Bárbara'),
(312, 48, 'San Pedro'),
(313, 48, 'San Juan'),
(314, 48, 'Jesús'),
(315, 48, 'Santo Domingo'),
(316, 48, 'Purabá'),

(317, 49, 'San Rafael'),
(318, 49, 'San Josecito'),
(319, 49, 'Santiago'),
(320, 49, 'Ángeles'),
(321, 49, 'Concepción'),

(322, 50, 'San Isidro'),
(323, 50, 'San José'),
(324, 50, 'Concepción'),
(325, 50, 'San Francisco'),

(326, 51, 'San Antonio'),
(327, 51, 'La Ribera'),
(328, 51, 'La Asunción'),

(329, 52, 'San Joaquín'),
(330, 52, 'Barrantes'),
(331, 52, 'Llorente'),

(332, 53, 'San Pablo'),
(333, 53, 'Rincón de Sabanilla'),

(334, 54, 'Puerto Viejo'),
(335, 54, 'La Virgen'),
(336, 54, 'Las Horquetas'),
(337, 54, 'Llanuras del Gaspar'),
(338, 54, 'Cureña'),

-- Guanacaste
(339, 55, 'Liberia'),
(340, 55, 'Cañas Dulces'),
(341, 55, 'Mayorga'),
(342, 55, 'Nacascolo'),
(343, 55, 'Curubandé'),

(344, 56, 'Nicoya'),
(345, 56, 'Mansión'),
(346, 56, 'San Antonio'),
(347, 56, 'Quebrada Honda'),
(348, 56, 'Sámara'),
(349, 56, 'Nosara'),
(350, 56, 'Belén de Nosarita'),

(351, 57, 'Santa Cruz'),
(352, 57, 'Bolsón'),
(353, 57, 'Veintisiete de Abril'),
(354, 57, 'Tempate'),
(355, 57, 'Cartagena'),
(356, 57, 'Cuajiniquil'),
(357, 57, 'Diriá'),
(358, 57, 'Cabo Velas'),
(359, 57, 'Tamarindo'),

(360, 58, 'Bagaces'),
(361, 58, 'La Fortuna'),
(362, 58, 'Mogote'),
(363, 58, 'Río Naranjo'),

(364, 59, 'Filadelfia'),
(365, 59, 'Palmira'),
(366, 59, 'Sardinal'),
(367, 59, 'Belén'),

(368, 60, 'Cañas'),
(369, 60, 'Palmira'),
(370, 60, 'San Miguel'),
(371, 60, 'Bebedero'),
(372, 60, 'Porozal'),

(373, 61, 'Las Juntas'),
(374, 61, 'Sierra'),
(375, 61, 'San Juan'),
(376, 61, 'Colorado'),

(377, 62, 'Tilarán'),
(378, 62, 'Quebrada Grande'),
(379, 62, 'Tronadora'),
(380, 62, 'Santa Rosa'),
(381, 62, 'Líbano'),
(382, 62, 'Tierras Morenas'),
(383, 62, 'Arenal'),
(384, 62, 'Cabeceras'),

(385, 63, 'Carmona'),
(386, 63, 'Santa Rita'),
(387, 63, 'Zapotal'),
(388, 63, 'San Pablo'),
(389, 63, 'Porvenir'),
(390, 63, 'Bejuco'),

(391, 64, 'La Cruz'),
(392, 64, 'Santa Cecilia'),
(393, 64, 'La Garita'),
(394, 64, 'Santa Elena'),

(395, 65, 'Hojancha'),
(396, 65, 'Monte Romo'),
(397, 65, 'Puerto Carrillo'),
(398, 65, 'Huacas'),
(399, 65, 'Matambú'),

-- Puntarenas
(400, 66, 'Puntarenas'),
(401, 66, 'Pitahaya'),
(402, 66, 'Chomes'),
(403, 66, 'Lepanto'),
(404, 66, 'Paquera'),
(405, 66, 'Manzanillo'),
(406, 66, 'Guacimal'),
(407, 66, 'Barranca'),
(408, 66, 'Isla del Coco'),
(409, 66, 'Cóbano'),
(410, 66, 'Chacarita'),
(411, 66, 'Chira'),
(412, 66, 'Acapulco'),
(413, 66, 'El Roble'),
(414, 66, 'Arancibia'),

(415, 67, 'Espíritu Santo'),
(416, 67, 'San Juan Grande'),
(417, 67, 'Macacona'),
(418, 67, 'San Rafael'),
(419, 67, 'San Jerónimo'),
(420, 67, 'Caldera'),

(421, 68, 'Buenos Aires'),
(422, 68, 'Volcán'),
(423, 68, 'Potrero Grande'),
(424, 68, 'Boruca'),
(425, 68, 'Pilas'),
(426, 68, 'Colinas'),
(427, 68, 'Chánguena'),
(428, 68, 'Biolley'),
(429, 68, 'Brunka'),

(430, 69, 'Miramar'),
(431, 69, 'La Unión'),
(432, 69, 'San Isidro'),

(433, 70, 'Puerto Cortés'),
(434, 70, 'Palmar'),
(435, 70, 'Sierpe'),
(436, 70, 'Bahía Ballena'),
(437, 70, 'Piedras Blancas'),
(438, 70, 'Bahía Drake'),

(439, 71, 'Quepos'),
(440, 71, 'Savegre'),
(441, 71, 'Naranjito'),

(442, 72, 'Golfito'),
(443, 72, 'Puerto Jiménez'),
(444, 72, 'Guaycará'),
(445, 72, 'Pavón'),

(446, 73, 'San Vito'),
(447, 73, 'Sabalito'),
(448, 73, 'Aguabuena'),
(449, 73, 'Limoncito'),
(450, 73, 'Pittier'),
(451, 73, 'Gutiérrez Braun'),

(452, 74, 'Parrita'),

(453, 75, 'Corredor'),
(454, 75, 'La Cuesta'),
(455, 75, 'Canoas'),
(456, 75, 'Laurel'),

(457, 76, 'Jacó'),
(458, 76, 'Tárcoles'),
(459, 76, 'Lagunillas'),

(460, 77, 'Monteverde'),

-- Limón
(461, 78, 'Limón'),
(462, 78, 'Valle La Estrella'),
(463, 78, 'Río Blanco'),
(464, 78, 'Matama'),

(465, 79, 'Guápiles'),
(466, 79, 'Jiménez'),
(467, 79, 'Rita'),
(468, 79, 'Roxana'),
(469, 79, 'Cariari'),
(470, 79, 'Colorado'),
(471, 79, 'La Colonia'),

(472, 80, 'Siquirres'),
(473, 80, 'Pacuarito'),
(474, 80, 'Florida'),
(475, 80, 'Germania'),
(476, 80, 'El Cairo'),
(477, 80, 'Alegría'),
(478, 80, 'Reventazón'),

(479, 81, 'Bratsi'),
(480, 81, 'Sixaola'),
(481, 81, 'Cahuita'),
(482, 81, 'Telire'),

(483, 82, 'Matina'),
(484, 82, 'Batán'),
(485, 82, 'Carrandí'),

(486, 83, 'Guácimo'),
(487, 83, 'Mercedes'),
(488, 83, 'Pocora');

SET IDENTITY_INSERT District OFF;


IF NOT EXISTS(SELECT 1 FROM [User] WHERE Id = 1)
BEGIN

    SET IDENTITY_INSERT [User] ON;

    INSERT INTO [User]
    (Id, CardId, FirstName, LastName, Telephone, Email, DistrictId, Birthday, [Password], GenderId, RoleId, Created,CreatedBy, Active)
    VALUES
    (1, '12345', 'Prueba', 'Prueba2', 123456, 'prueba@gmail.com', 1, '1990-12-12', '827CCB0EEA8A706C4C34A16891F84E7B', 1, 1, '2025-06-02', 'ADMIN',1)

    SET IDENTITY_INSERT [User] OFF;
END