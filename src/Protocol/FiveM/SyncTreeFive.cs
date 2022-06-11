using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.FiveM
{
    public static class SyncTreeFive
    {
        public static readonly SyncTree CAutomobileSyncTree =
            new SyncTree(
                new ParentNode(
                    new NodeIds(127, 0, 0),
                    new ParentNode(
                        new NodeIds(1, 0, 0),
                        new NodeWrapper(new NodeIds(1, 0, 0), new CVehicleCreationDataNode(), 14),
                        new NodeWrapper(new NodeIds(1, 0, 0), new CAutomobileCreationDataNode(), 2)
                    ),
                    new ParentNode(
                        new NodeIds(127, 127, 0),
                        new ParentNode(
                            new NodeIds(127, 127, 0),
                            new ParentNode(
                                new NodeIds(127, 127, 0),
                                new NodeWrapper(new NodeIds(127, 127, 0), new CGlobalFlagsDataNode(), 2),
                                new NodeWrapper(new NodeIds(127, 127, 0), new CDynamicEntityGameStateDataNode(), 102),
                                new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalGameStateDataNode(), 4),
                                new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleGameStateDataNode(), 56)
                            ),
                            new ParentNode(
                                new NodeIds(127, 127, 1),
                                new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptGameStateDataNode(), 1),
                                new NodeWrapper(new NodeIds(127, 127, 1), new CPhysicalScriptGameStateDataNode(), 13),
                                new NodeWrapper(new NodeIds(127, 127, 1), new CVehicleScriptGameStateDataNode(), 48),
                                new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptInfoDataNode(), 24)
                            )
                        ),
                        new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalAttachDataNode(), 28),
                        new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleAppearanceDataNode(), 179),
                        new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleDamageStatusDataNode(), 34),
                        new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleComponentReservationDataNode(), 65),
                        new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleHealthDataNode(), 57),
                        new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleTaskDataNode(), 34)
                    ),
                    new ParentNode(
                        new NodeIds(127, 86, 0),
                        new NodeWrapper(new NodeIds(87, 87, 0), new CSectorDataNode(), 4),
                        new NodeWrapper(new NodeIds(87, 87, 0), new CSectorPositionDataNode(), 5),
                        new NodeWrapper(new NodeIds(87, 87, 0), new CEntityOrientationDataNode(), 5),
                        new NodeWrapper(new NodeIds(87, 87, 0), new CPhysicalVelocityDataNode(), 5),
                        new NodeWrapper(new NodeIds(87, 87, 0), new CVehicleAngVelocityDataNode(), 4),
                        new ParentNode(
                            new NodeIds(127, 86, 0),
                            new NodeWrapper(new NodeIds(86, 86, 0), new CVehicleSteeringDataNode(), 2),
                            new NodeWrapper(new NodeIds(87, 87, 0), new CVehicleControlDataNode(), 27),
                            new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleGadgetDataNode(), 30)
                        )
                    ),
                    new ParentNode(
                        new NodeIds(4, 0, 0),
                        new NodeWrapper(new NodeIds(4, 0, 0), new CMigrationDataNode(), 13),
                        new NodeWrapper(new NodeIds(4, 0, 0), new CPhysicalMigrationDataNode(), 1),
                        new NodeWrapper(new NodeIds(4, 0, 1), new CPhysicalScriptMigrationDataNode(), 1),
                        new NodeWrapper(new NodeIds(4, 0, 0), new CVehicleProximityMigrationDataNode(), 36)
                    )
                )
            );

		public static readonly SyncTree CBikeSyncTree = new SyncTree(
			new ParentNode(
				new NodeIds(127, 0, 0),
				new ParentNode(
					new NodeIds(1, 0, 0),
					new NodeWrapper(new NodeIds(1, 0, 0), new CVehicleCreationDataNode(), 14)
				),
				new ParentNode(
					new NodeIds(127, 127, 0),
					new ParentNode(
						new NodeIds(127, 127, 0),
						new ParentNode(
							new NodeIds(127, 127, 0),
							new NodeWrapper(new NodeIds(127, 127, 0), new CGlobalFlagsDataNode(), 2),
							new NodeWrapper(new NodeIds(127, 127, 0), new CDynamicEntityGameStateDataNode(), 102),
							new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalGameStateDataNode(), 4),
							new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleGameStateDataNode(), 56),
							new NodeWrapper(new NodeIds(127, 127, 0), new CBikeGameStateDataNode(), 1)
						),
						new ParentNode(
							new NodeIds(127, 127, 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptGameStateDataNode(), 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CPhysicalScriptGameStateDataNode(), 13),
							new NodeWrapper(new NodeIds(127, 127, 1), new CVehicleScriptGameStateDataNode(), 48),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptInfoDataNode(), 24)
						)
					),
					new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalAttachDataNode(), 28),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleAppearanceDataNode(), 179),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleDamageStatusDataNode(), 34),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleComponentReservationDataNode(), 65),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleHealthDataNode(), 57),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleTaskDataNode(), 34)
				),
				new ParentNode(
					new NodeIds(127, 86, 0),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorDataNode(), 4),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorPositionDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CEntityOrientationDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPhysicalVelocityDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CVehicleAngVelocityDataNode(), 4),
					new ParentNode(
						new NodeIds(127, 86, 0),
						new NodeWrapper(new NodeIds(86, 86, 0), new CVehicleSteeringDataNode(), 2),
						new NodeWrapper(new NodeIds(87, 87, 0), new CVehicleControlDataNode(), 27),
						new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleGadgetDataNode(), 30)
					)
				),
				new ParentNode(
					new NodeIds(4, 0, 0),
					new NodeWrapper(new NodeIds(4, 0, 0), new CMigrationDataNode(), 13),
					new NodeWrapper(new NodeIds(4, 0, 0), new CPhysicalMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 1), new CPhysicalScriptMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 0), new CVehicleProximityMigrationDataNode(), 36)
				)
			)
		);
		public static readonly SyncTree CBoatSyncTree = new SyncTree(
			new ParentNode(
				new NodeIds(127, 0, 0),
				new ParentNode(
					new NodeIds(1, 0, 0),
					new NodeWrapper(new NodeIds(1, 0, 0), new CVehicleCreationDataNode(), 14)
				),
				new ParentNode(
					new NodeIds(127, 87, 0),
					new ParentNode(
						new NodeIds(127, 87, 0),
						new ParentNode(
							new NodeIds(127, 87, 0),
							new NodeWrapper(new NodeIds(127, 127, 0), new CGlobalFlagsDataNode(), 2),
							new NodeWrapper(new NodeIds(127, 127, 0), new CDynamicEntityGameStateDataNode(), 102),
							new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalGameStateDataNode(), 4),
							new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleGameStateDataNode(), 56),
							new NodeWrapper(new NodeIds(87, 87, 0), new CBoatGameStateDataNode(), 5)
						),
						new ParentNode(
							new NodeIds(127, 127, 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptGameStateDataNode(), 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CPhysicalScriptGameStateDataNode(), 13),
							new NodeWrapper(new NodeIds(127, 127, 1), new CVehicleScriptGameStateDataNode(), 48),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptInfoDataNode(), 24)
						)
					),
					new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalAttachDataNode(), 28),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleAppearanceDataNode(), 179),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleDamageStatusDataNode(), 34),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleComponentReservationDataNode(), 65),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleHealthDataNode(), 57),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleTaskDataNode(), 34)
				),
				new ParentNode(
					new NodeIds(127, 86, 0),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorDataNode(), 4),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorPositionDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CEntityOrientationDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPhysicalVelocityDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CVehicleAngVelocityDataNode(), 4),
					new ParentNode(
						new NodeIds(127, 86, 0),
						new NodeWrapper(new NodeIds(86, 86, 0), new CVehicleSteeringDataNode(), 2),
						new NodeWrapper(new NodeIds(87, 87, 0), new CVehicleControlDataNode(), 27),
						new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleGadgetDataNode(), 30)
					)
				),
				new ParentNode(
					new NodeIds(4, 0, 0),
					new NodeWrapper(new NodeIds(4, 0, 0), new CMigrationDataNode(), 13),
					new NodeWrapper(new NodeIds(4, 0, 0), new CPhysicalMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 1), new CPhysicalScriptMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 0), new CVehicleProximityMigrationDataNode(), 36)
				)
			)
		);
		public static readonly SyncTree CDoorSyncTree = new SyncTree(
			new ParentNode(
				new NodeIds(127, 0, 0),
				new ParentNode(
					new NodeIds(1, 0, 0),
					new NodeWrapper(new NodeIds(1, 0, 0), new CDoorCreationDataNode(), 12)
				),
				new ParentNode(
					new NodeIds(127, 127, 0),
					new NodeWrapper(new NodeIds(127, 127, 0), new CGlobalFlagsDataNode(), 2),
					new NodeWrapper(new NodeIds(127, 127, 1), new CDoorScriptInfoDataNode(), 28),
					new NodeWrapper(new NodeIds(127, 127, 1), new CDoorScriptGameStateDataNode(), 8)
				),
				new NodeWrapper(new NodeIds(86, 86, 0), new CDoorMovementDataNode(), 2),
				new ParentNode(
					new NodeIds(4, 0, 0),
					new NodeWrapper(new NodeIds(4, 0, 0), new CMigrationDataNode(), 13),
					new NodeWrapper(new NodeIds(4, 0, 1), new CPhysicalScriptMigrationDataNode(), 1)
				)
			)
		);
		public static readonly SyncTree CHeliSyncTree = new SyncTree(
			new ParentNode(
				new NodeIds(127, 0, 0),
				new ParentNode(
					new NodeIds(1, 0, 0),
					new NodeWrapper(new NodeIds(1, 0, 0), new CVehicleCreationDataNode(), 14),
					new NodeWrapper(new NodeIds(1, 0, 0), new CAutomobileCreationDataNode(), 2)
				),
				new ParentNode(
					new NodeIds(127, 87, 0),
					new ParentNode(
						new NodeIds(127, 127, 0),
						new ParentNode(
							new NodeIds(127, 127, 0),
							new NodeWrapper(new NodeIds(127, 127, 0), new CGlobalFlagsDataNode(), 2),
							new NodeWrapper(new NodeIds(127, 127, 0), new CDynamicEntityGameStateDataNode(), 102),
							new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalGameStateDataNode(), 4),
							new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleGameStateDataNode(), 56)
						),
						new ParentNode(
							new NodeIds(127, 127, 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptGameStateDataNode(), 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CPhysicalScriptGameStateDataNode(), 13),
							new NodeWrapper(new NodeIds(127, 127, 1), new CVehicleScriptGameStateDataNode(), 48),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptInfoDataNode(), 24)
						)
					),
					new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalAttachDataNode(), 28),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleAppearanceDataNode(), 179),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleDamageStatusDataNode(), 34),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleComponentReservationDataNode(), 65),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleHealthDataNode(), 57),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleTaskDataNode(), 34),
					new NodeWrapper(new NodeIds(87, 87, 0), new CHeliHealthDataNode(), 16)
				),
				new ParentNode(
					new NodeIds(127, 86, 0),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorDataNode(), 4),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorPositionDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CEntityOrientationDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPhysicalVelocityDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CVehicleAngVelocityDataNode(), 4),
					new ParentNode(
						new NodeIds(127, 86, 0),
						new NodeWrapper(new NodeIds(86, 86, 0), new CVehicleSteeringDataNode(), 2),
						new NodeWrapper(new NodeIds(87, 87, 0), new CVehicleControlDataNode(), 27),
						new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleGadgetDataNode(), 30),
						new NodeWrapper(new NodeIds(86, 86, 0), new CHeliControlDataNode(), 8)
					)
				),
				new ParentNode(
					new NodeIds(4, 0, 0),
					new NodeWrapper(new NodeIds(4, 0, 0), new CMigrationDataNode(), 13),
					new NodeWrapper(new NodeIds(4, 0, 0), new CPhysicalMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 1), new CPhysicalScriptMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 0), new CVehicleProximityMigrationDataNode(), 36)
				)
			)
		);
		public static readonly SyncTree CObjectSyncTree = new SyncTree(
			new ParentNode(
				new NodeIds(127, 0, 0),
				new ParentNode(
					new NodeIds(1, 0, 0),
					new NodeWrapper(new NodeIds(1, 0, 0), new CObjectCreationDataNode(), 18)
				),
				new ParentNode(
					new NodeIds(127, 127, 0),
					new ParentNode(
						new NodeIds(127, 127, 0),
						new ParentNode(
							new NodeIds(127, 127, 0),
							new NodeWrapper(new NodeIds(127, 127, 0), new CGlobalFlagsDataNode(), 2),
							new NodeWrapper(new NodeIds(127, 127, 0), new CDynamicEntityGameStateDataNode(), 102),
							new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalGameStateDataNode(), 4),
							new NodeWrapper(new NodeIds(127, 127, 0), new CObjectGameStateDataNode(), 44)
						),
						new ParentNode(
							new NodeIds(127, 127, 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptGameStateDataNode(), 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CPhysicalScriptGameStateDataNode(), 13),
							new NodeWrapper(new NodeIds(127, 127, 1), new CObjectScriptGameStateDataNode(), 12),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptInfoDataNode(), 24)
						)
					),
					new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalAttachDataNode(), 28),
					new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalHealthDataNode(), 19)
				),
				new ParentNode(
					new NodeIds(87, 87, 0),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorDataNode(), 4),
					new NodeWrapper(new NodeIds(87, 87, 0), new CObjectSectorPosNode(), 8),
					new NodeWrapper(new NodeIds(87, 87, 0), new CObjectOrientationDataNode(), 8),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPhysicalVelocityDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPhysicalAngVelocityDataNode(), 4)
				),
				new ParentNode(
					new NodeIds(4, 0, 0),
					new NodeWrapper(new NodeIds(4, 0, 0), new CMigrationDataNode(), 13),
					new NodeWrapper(new NodeIds(4, 0, 0), new CPhysicalMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 1), new CPhysicalScriptMigrationDataNode(), 1)
				)
			)
		);
		public static readonly SyncTree CPedSyncTree = new SyncTree(
			new ParentNode(
				new NodeIds(127, 0, 0),
				new ParentNode(
					new NodeIds(1, 0, 0),
					new NodeWrapper(new NodeIds(1, 0, 0), new CPedCreationDataNode(), 20),
					new NodeWrapper(new NodeIds(1, 0, 1), new CPedScriptCreationDataNode(), 1)
				),
				new ParentNode(
					new NodeIds(127, 87, 0),
					new ParentNode(
						new NodeIds(127, 127, 0),
						new ParentNode(
							new NodeIds(127, 127, 0),
							new NodeWrapper(new NodeIds(127, 127, 0), new CGlobalFlagsDataNode(), 2),
							new NodeWrapper(new NodeIds(127, 127, 0), new CDynamicEntityGameStateDataNode(), 102),
							new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalGameStateDataNode(), 4),
							new NodeWrapper(new NodeIds(127, 127, 0), new CPedGameStateDataNode(), 98),
							new NodeWrapper(new NodeIds(127, 127, 0), new CPedComponentReservationDataNode(), 65)
						),
						new ParentNode(
							new NodeIds(127, 127, 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptGameStateDataNode(), 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CPhysicalScriptGameStateDataNode(), 13),
							new NodeWrapper(new NodeIds(127, 127, 1), new CPedScriptGameStateDataNode(), 108),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptInfoDataNode(), 24)
						)
					),
					new NodeWrapper(new NodeIds(127, 127, 1), new CPedAttachDataNode(), 22),
					new NodeWrapper(new NodeIds(127, 127, 0), new CPedHealthDataNode(), 17),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPedMovementGroupDataNode(), 26),
					new NodeWrapper(new NodeIds(127, 127, 1), new CPedAIDataNode(), 9),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPedAppearanceDataNode(), 141)
				),
				new ParentNode(
					new NodeIds(127, 87, 0),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPedOrientationDataNode(), 3),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPedMovementDataNode(), 5),
					new ParentNode(
						new NodeIds(127, 87, 0),
						new NodeWrapper(new NodeIds(127, 127, 0), new CPedTaskTreeDataNode(), 28),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77)
					),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorDataNode(), 4),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPedSectorPosMapNode(), 12),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPedSectorPosNavMeshNode(), 4)
				),
				new ParentNode(
					new NodeIds(5, 0, 0),
					new NodeWrapper(new NodeIds(4, 0, 0), new CMigrationDataNode(), 13),
					new NodeWrapper(new NodeIds(4, 0, 0), new CPhysicalMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 1), new CPhysicalScriptMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(5, 0, 0), new CPedInventoryDataNode(), 316),
					new NodeWrapper(new NodeIds(4, 4, 1), new CPedTaskSequenceDataNode(), 1)
				)
			)
		);
		public static readonly SyncTree CPickupSyncTree = new SyncTree(
			new ParentNode(
				new NodeIds(127, 0, 0),
				new ParentNode(
					new NodeIds(1, 0, 0),
					new NodeWrapper(new NodeIds(1, 0, 0), new CPickupCreationDataNode(), 62)
				),
				new ParentNode(
					new NodeIds(127, 127, 0),
					new ParentNode(
						new NodeIds(127, 127, 0),
						new NodeWrapper(new NodeIds(127, 127, 0), new CGlobalFlagsDataNode(), 2),
						new NodeWrapper(new NodeIds(127, 127, 0), new CDynamicEntityGameStateDataNode(), 102)
					),
					new ParentNode(
						new NodeIds(127, 127, 1),
						new NodeWrapper(new NodeIds(127, 127, 1), new CPickupScriptGameStateNode(), 14),
						new NodeWrapper(new NodeIds(127, 127, 1), new CPhysicalGameStateDataNode(), 4),
						new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptGameStateDataNode(), 1),
						new NodeWrapper(new NodeIds(127, 127, 1), new CPhysicalScriptGameStateDataNode(), 13),
						new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptInfoDataNode(), 24),
						new NodeWrapper(new NodeIds(127, 127, 1), new CPhysicalHealthDataNode(), 19)
					),
					new NodeWrapper(new NodeIds(127, 127, 1), new CPhysicalAttachDataNode(), 28)
				),
				new ParentNode(
					new NodeIds(87, 87, 0),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorDataNode(), 4),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPickupSectorPosNode(), 8),
					new NodeWrapper(new NodeIds(87, 87, 0), new CEntityOrientationDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPhysicalVelocityDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPhysicalAngVelocityDataNode(), 4)
				),
				new ParentNode(
					new NodeIds(4, 0, 0),
					new NodeWrapper(new NodeIds(4, 0, 0), new CMigrationDataNode(), 13),
					new NodeWrapper(new NodeIds(4, 0, 1), new CPhysicalMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 1), new CPhysicalScriptMigrationDataNode(), 1)
				)
			)
		);
		public static readonly SyncTree CPickupPlacementSyncTree = new SyncTree(
			new ParentNode(
				new NodeIds(127, 0, 0),
				new NodeWrapper(new NodeIds(1, 0, 0), new CPickupPlacementCreationDataNode(), 54),
				new NodeWrapper(new NodeIds(4, 0, 0), new CMigrationDataNode(), 13),
				new NodeWrapper(new NodeIds(127, 127, 0), new CGlobalFlagsDataNode(), 2),
				new NodeWrapper(new NodeIds(127, 127, 0), new CPickupPlacementStateDataNode(), 7)
			)
		);
		public static readonly SyncTree CPlaneSyncTree = new SyncTree(
			new ParentNode(
				new NodeIds(127, 0, 0),
				new ParentNode(
					new NodeIds(1, 0, 0),
					new NodeWrapper(new NodeIds(1, 0, 0), new CVehicleCreationDataNode(), 14)
				),
				new ParentNode(
					new NodeIds(127, 127, 0),
					new ParentNode(
						new NodeIds(127, 127, 0),
						new ParentNode(
							new NodeIds(127, 127, 0),
							new NodeWrapper(new NodeIds(127, 127, 0), new CGlobalFlagsDataNode(), 2),
							new NodeWrapper(new NodeIds(127, 127, 0), new CDynamicEntityGameStateDataNode(), 102),
							new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalGameStateDataNode(), 4),
							new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleGameStateDataNode(), 56)
						),
						new ParentNode(
							new NodeIds(127, 127, 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptGameStateDataNode(), 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CPhysicalScriptGameStateDataNode(), 13),
							new NodeWrapper(new NodeIds(127, 127, 1), new CVehicleScriptGameStateDataNode(), 48),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptInfoDataNode(), 24)
						)
					),
					new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalAttachDataNode(), 28),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleAppearanceDataNode(), 179),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleDamageStatusDataNode(), 34),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleComponentReservationDataNode(), 65),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleHealthDataNode(), 57),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleTaskDataNode(), 34),
					new NodeWrapper(new NodeIds(127, 127, 0), new CPlaneGameStateDataNode(), 52)
				),
				new ParentNode(
					new NodeIds(127, 86, 0),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorDataNode(), 4),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorPositionDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CEntityOrientationDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPhysicalVelocityDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CVehicleAngVelocityDataNode(), 4),
					new ParentNode(
						new NodeIds(127, 86, 0),
						new NodeWrapper(new NodeIds(86, 86, 0), new CVehicleSteeringDataNode(), 2),
						new NodeWrapper(new NodeIds(87, 87, 0), new CVehicleControlDataNode(), 27),
						new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleGadgetDataNode(), 30),
						new NodeWrapper(new NodeIds(86, 86, 0), new CPlaneControlDataNode(), 7)
					)
				),
				new ParentNode(
					new NodeIds(4, 0, 0),
					new NodeWrapper(new NodeIds(4, 0, 0), new CMigrationDataNode(), 13),
					new NodeWrapper(new NodeIds(4, 0, 0), new CPhysicalMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 1), new CPhysicalScriptMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 0), new CVehicleProximityMigrationDataNode(), 36)
				)
			)
		);
		public static readonly SyncTree CSubmarineSyncTree = new SyncTree(
			new ParentNode(
				new NodeIds(127, 0, 0),
				new ParentNode(
					new NodeIds(1, 0, 0),
					new NodeWrapper(new NodeIds(1, 0, 0), new CVehicleCreationDataNode(), 14)
				),
				new ParentNode(
					new NodeIds(127, 87, 0),
					new ParentNode(
						new NodeIds(127, 87, 0),
						new ParentNode(
							new NodeIds(127, 87, 0),
							new NodeWrapper(new NodeIds(127, 127, 0), new CGlobalFlagsDataNode(), 2),
							new NodeWrapper(new NodeIds(127, 127, 0), new CDynamicEntityGameStateDataNode(), 102),
							new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalGameStateDataNode(), 4),
							new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleGameStateDataNode(), 56),
							new NodeWrapper(new NodeIds(87, 87, 0), new CSubmarineGameStateDataNode(), 1)
						),
						new ParentNode(
							new NodeIds(127, 127, 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptGameStateDataNode(), 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CPhysicalScriptGameStateDataNode(), 13),
							new NodeWrapper(new NodeIds(127, 127, 1), new CVehicleScriptGameStateDataNode(), 48),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptInfoDataNode(), 24)
						)
					),
					new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalAttachDataNode(), 28),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleAppearanceDataNode(), 179),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleDamageStatusDataNode(), 34),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleComponentReservationDataNode(), 65),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleHealthDataNode(), 57),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleTaskDataNode(), 34)
				),
				new ParentNode(
					new NodeIds(127, 86, 0),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorDataNode(), 4),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorPositionDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CEntityOrientationDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPhysicalVelocityDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CVehicleAngVelocityDataNode(), 4),
					new ParentNode(
						new NodeIds(127, 86, 0),
						new NodeWrapper(new NodeIds(86, 86, 0), new CVehicleSteeringDataNode(), 2),
						new NodeWrapper(new NodeIds(87, 87, 0), new CVehicleControlDataNode(), 27),
						new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleGadgetDataNode(), 30),
						new NodeWrapper(new NodeIds(86, 86, 0), new CSubmarineControlDataNode(), 4)
					)
				),
				new ParentNode(
					new NodeIds(4, 0, 0),
					new NodeWrapper(new NodeIds(4, 0, 0), new CMigrationDataNode(), 13),
					new NodeWrapper(new NodeIds(4, 0, 0), new CPhysicalMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 1), new CPhysicalScriptMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 0), new CVehicleProximityMigrationDataNode(), 36)
				)
			)
		);
		public static readonly SyncTree CPlayerSyncTree = new SyncTree(
			new ParentNode(
				new NodeIds(127, 0, 0),
				new ParentNode(
					new NodeIds(1, 0, 0),
					new NodeWrapper(new NodeIds(1, 0, 0), new CPlayerCreationDataNode(), 128)
				),
				new ParentNode(
					new NodeIds(127, 86, 0),
					new ParentNode(
						new NodeIds(127, 87, 0),
						new ParentNode(
							new NodeIds(127, 127, 0),
							new NodeWrapper(new NodeIds(127, 127, 0), new CGlobalFlagsDataNode(), 2),
							new NodeWrapper(new NodeIds(127, 127, 0), new CDynamicEntityGameStateDataNode(), 102),
							new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalGameStateDataNode(), 4),
							new NodeWrapper(new NodeIds(127, 127, 0), new CPedGameStateDataNode(), 98),
							new NodeWrapper(new NodeIds(127, 127, 0), new CPedComponentReservationDataNode(), 65)
						),
						new ParentNode(
							new NodeIds(127, 87, 0),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptGameStateDataNode(), 1),
							new NodeWrapper(new NodeIds(87, 87, 0), new CPlayerGameStateDataNode(), 102)
						)
					),
					new NodeWrapper(new NodeIds(127, 127, 1), new CPedAttachDataNode(), 22),
					new NodeWrapper(new NodeIds(127, 127, 0), new CPedHealthDataNode(), 17),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPedMovementGroupDataNode(), 26),
					new NodeWrapper(new NodeIds(127, 127, 1), new CPedAIDataNode(), 9),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPlayerAppearanceDataNode(), 528),
					new NodeWrapper(new NodeIds(86, 86, 0), new CPlayerPedGroupDataNode(), 19),
					new NodeWrapper(new NodeIds(86, 86, 0), new CPlayerAmbientModelStreamingNode(), 5),
					new NodeWrapper(new NodeIds(86, 86, 0), new CPlayerGamerDataNode(), 325),
					new NodeWrapper(new NodeIds(86, 86, 0), new CPlayerExtendedGameStateNode(), 20)
				),
				new ParentNode(
					new NodeIds(127, 86, 0),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPedOrientationDataNode(), 3),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPedMovementDataNode(), 5),
					new ParentNode(
						new NodeIds(127, 87, 0),
						new NodeWrapper(new NodeIds(127, 127, 0), new CPedTaskTreeDataNode(), 28),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77),
						new NodeWrapper(new NodeIds(87, 87, 0), new CPedTaskSpecificDataNode(), 77)
					),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorDataNode(), 4),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPlayerSectorPosNode(), 13),
					new NodeWrapper(new NodeIds(86, 86, 0), new CPlayerCameraDataNode(), 24),
					new NodeWrapper(new NodeIds(86, 86, 0), new CPlayerWantedAndLOSDataNode(), 30)
				),
				new ParentNode(
					new NodeIds(4, 0, 0),
					new NodeWrapper(new NodeIds(4, 0, 0), new CMigrationDataNode(), 13),
					new NodeWrapper(new NodeIds(4, 0, 0), new CPhysicalMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 1), new CPhysicalScriptMigrationDataNode(), 1)
				)
			)
		);

		public static readonly SyncTree CTrainSyncTree = new SyncTree(
			new ParentNode(
				new NodeIds(127, 0, 0),
				new ParentNode(
					new NodeIds(1, 0, 0),
					new NodeWrapper(new NodeIds(1, 0, 0), new CVehicleCreationDataNode(), 14)
				),
				new ParentNode(
					new NodeIds(127, 127, 0),
					new ParentNode(
						new NodeIds(127, 127, 0),
						new ParentNode(
							new NodeIds(127, 127, 0),
							new NodeWrapper(new NodeIds(127, 127, 0), new CGlobalFlagsDataNode(), 2),
							new NodeWrapper(new NodeIds(127, 127, 0), new CDynamicEntityGameStateDataNode(), 102),
							new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalGameStateDataNode(), 4),
							new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleGameStateDataNode(), 56),
							new NodeWrapper(new NodeIds(127, 127, 0), new CTrainGameStateDataNode(), 16)
						),
						new ParentNode(
							new NodeIds(127, 127, 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptGameStateDataNode(), 1),
							new NodeWrapper(new NodeIds(127, 127, 1), new CPhysicalScriptGameStateDataNode(), 13),
							new NodeWrapper(new NodeIds(127, 127, 1), new CVehicleScriptGameStateDataNode(), 48),
							new NodeWrapper(new NodeIds(127, 127, 1), new CEntityScriptInfoDataNode(), 24)
						)
					),
					new NodeWrapper(new NodeIds(127, 127, 0), new CPhysicalAttachDataNode(), 28),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleAppearanceDataNode(), 179),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleDamageStatusDataNode(), 34),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleComponentReservationDataNode(), 65),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleHealthDataNode(), 57),
					new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleTaskDataNode(), 34)
				),
				new ParentNode(
					new NodeIds(127, 86, 0),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorDataNode(), 4),
					new NodeWrapper(new NodeIds(87, 87, 0), new CSectorPositionDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CEntityOrientationDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CPhysicalVelocityDataNode(), 5),
					new NodeWrapper(new NodeIds(87, 87, 0), new CVehicleAngVelocityDataNode(), 4),
					new ParentNode(
						new NodeIds(127, 86, 0),
						new NodeWrapper(new NodeIds(86, 86, 0), new CVehicleSteeringDataNode(), 2),
						new NodeWrapper(new NodeIds(87, 87, 0), new CVehicleControlDataNode(), 27),
						new NodeWrapper(new NodeIds(127, 127, 0), new CVehicleGadgetDataNode(), 30)
					)
				),
				new ParentNode(
					new NodeIds(4, 0, 0),
					new NodeWrapper(new NodeIds(4, 0, 0), new CMigrationDataNode(), 13),
					new NodeWrapper(new NodeIds(4, 0, 0), new CPhysicalMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 1), new CPhysicalScriptMigrationDataNode(), 1),
					new NodeWrapper(new NodeIds(4, 0, 0), new CVehicleProximityMigrationDataNode(), 36)
				)
			)
		);

	}

	public class CVehicleCreationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CAutomobileCreationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CGlobalFlagsDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CDynamicEntityGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPhysicalGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CVehicleGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CEntityScriptGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPhysicalScriptGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CVehicleScriptGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CEntityScriptInfoDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPhysicalAttachDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CVehicleAppearanceDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CVehicleDamageStatusDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CVehicleComponentReservationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CVehicleHealthDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CVehicleTaskDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CSectorDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CSectorPositionDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CEntityOrientationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPhysicalVelocityDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CVehicleAngVelocityDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CVehicleSteeringDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CVehicleControlDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CVehicleGadgetDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CMigrationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPhysicalMigrationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPhysicalScriptMigrationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CVehicleProximityMigrationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CBikeGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CBoatGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CDoorCreationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CDoorScriptInfoDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CDoorScriptGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CDoorMovementDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CHeliHealthDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CHeliControlDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CObjectCreationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CObjectGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CObjectScriptGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPhysicalHealthDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CObjectSectorPosNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CObjectOrientationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPhysicalAngVelocityDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedCreationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedScriptCreationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedComponentReservationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedScriptGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedAttachDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedHealthDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedMovementGroupDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedAIDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedAppearanceDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedOrientationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedMovementDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedTaskTreeDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedTaskSpecificDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedSectorPosMapNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedSectorPosNavMeshNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedInventoryDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPedTaskSequenceDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPickupCreationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPickupScriptGameStateNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPickupSectorPosNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPickupPlacementCreationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPickupPlacementStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPlaneGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPlaneControlDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CSubmarineGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CSubmarineControlDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPlayerCreationDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPlayerGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPlayerAppearanceDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPlayerPedGroupDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPlayerAmbientModelStreamingNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPlayerGamerDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPlayerExtendedGameStateNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPlayerSectorPosNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPlayerCameraDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CPlayerWantedAndLOSDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

	public class CTrainGameStateDataNode : DataNode
	{
		public override bool Parse()
		{
			throw new NotImplementedException();
		}

		public override bool Unparse()
		{
			throw new NotImplementedException();
		}
	}

}
