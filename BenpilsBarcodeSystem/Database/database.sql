USE [BenpilsMotorcycleDatabase];
GO

CREATE TABLE [dbo].[tbl_user_credentials](
    [id] [int] IDENTITY(1,1) NOT NULL,
    [first_name][varchar](100)NOT NULL,
    [last_name][varchar](100)NULL,
    [username][varchar](100)NOT NULL,
    [password][varchar](100)NOT NULL,
    [designation][varchar](100)NULL,
    [address][varchar](255)NULL,
    [contact_no][varchar](50)NULL,
    [is_active] [bit] NULL,
    [date_created] [datetime] NULL,
    [date_updated] [datetime] NULL,
    PRIMARY KEY CLUSTERED 
    (
        [id] ASC
    )
);
GO

ALTER TABLE [dbo].[tbl_user_credentials] ADD  DEFAULT ('CASHIER') FOR [designation];
ALTER TABLE [dbo].[tbl_user_credentials] ADD  DEFAULT ('N/A') FOR [address];
ALTER TABLE [dbo].[tbl_user_credentials] ADD  DEFAULT ('N/A') FOR [contact_no];
ALTER TABLE [dbo].[tbl_user_credentials] ADD  DEFAULT ((1)) FOR [is_active];
ALTER TABLE [dbo].[tbl_user_credentials] ADD  DEFAULT (getdate()) FOR [date_created];
GO

CREATE TABLE [dbo].[tbl_item_master_data](
    [id] [int] IDENTITY(1,1) NOT NULL,
    [barcode][varchar](50)NOT NULL,
    [item_name][varchar](100)NOT NULL,
    [brand][varchar](50)NULL,
    [motor_brand][varchar](50)NULL,
    [purchase_price] [decimal](18, 2) NULL,
    [selling_price] [decimal](18, 2) NULL,
    [quantity] [int] NULL,
    [category][varchar](50)NULL,
    [size][varchar](100)NULL,
    [is_active] [bit] NULL,
    [date_created] [datetime] NULL,
    [date_updated] [datetime] NULL,
    PRIMARY KEY CLUSTERED 
    (
        [id] ASC
    )
);
GO

ALTER TABLE [dbo].[tbl_item_master_data] ADD  DEFAULT ('N/A') FOR [brand];
ALTER TABLE [dbo].[tbl_item_master_data] ADD  DEFAULT ('N/A') FOR [motor_brand];
ALTER TABLE [dbo].[tbl_item_master_data] ADD  DEFAULT ((0)) FOR [purchase_price];
ALTER TABLE [dbo].[tbl_item_master_data] ADD  DEFAULT ((0)) FOR [selling_price];
ALTER TABLE [dbo].[tbl_item_master_data] ADD  DEFAULT ((0)) FOR [quantity];
ALTER TABLE [dbo].[tbl_item_master_data] ADD  DEFAULT ('Uncategorized') FOR [category];
ALTER TABLE [dbo].[tbl_item_master_data] ADD  DEFAULT ('N/A') FOR [size];
ALTER TABLE [dbo].[tbl_item_master_data] ADD  DEFAULT ((1)) FOR [is_active];
ALTER TABLE [dbo].[tbl_item_master_data] ADD  DEFAULT (getdate()) FOR [date_created];
GO

CREATE TABLE [dbo].[tbl_suppliers](
    [supplier_id] [int] IDENTITY(1,1) NOT NULL,
    [contact_name][varchar](100)NOT NULL,
    [contact_no][varchar](50)NULL,
    [address][varchar](255)NULL,
    [is_active] [bit] NULL,
    [date_created] [datetime] NULL,
    [date_updated] [datetime] NULL,
    PRIMARY KEY CLUSTERED 
    (
        [supplier_id] ASC
    )
);
GO

ALTER TABLE [dbo].[tbl_suppliers] ADD  DEFAULT ('N/A') FOR [contact_no];
ALTER TABLE [dbo].[tbl_suppliers] ADD  DEFAULT ('N/A') FOR [address];
ALTER TABLE [dbo].[tbl_suppliers] ADD  DEFAULT ((1)) FOR [is_active];
ALTER TABLE [dbo].[tbl_suppliers] ADD  DEFAULT (getdate()) FOR [date_created];
GO

CREATE TABLE [dbo].[tbl_purchase_order](
    [order_id] [int] NOT NULL,
    [supplier_id] [int] NULL,
    [order_date] [datetime] NULL,
    [receiving_date] [date] NULL,
    [fulfillment_date] [datetime] NULL,
    [operated_by] [int] NULL,
    [fulfilled_by] [int] NULL,
    [is_backorder] [bit] NULL,
    [remarks] [varchar](max) NULL,
    [status][varchar](20)NULL,
    [backorder_from] [int] NULL,
    PRIMARY KEY CLUSTERED 
    (
        [order_id] ASC
    )
);
GO

ALTER TABLE [dbo].[tbl_purchase_order] ADD  CONSTRAINT [DF__tbl_purch__is_ba__6BE40491]  DEFAULT ((0)) FOR [is_backorder];
ALTER TABLE [dbo].[tbl_purchase_order] ADD  CONSTRAINT [DF__tbl_purch__statu__6CD828CA]  DEFAULT ('PENDING') FOR [status];
ALTER TABLE [dbo].[tbl_purchase_order] ADD  CONSTRAINT [FK__tbl_purch__suppl__4A8310C6] FOREIGN KEY([supplier_id])
REFERENCES [dbo].[tbl_suppliers] ([supplier_id]);
ALTER TABLE [dbo].[tbl_purchase_order] ADD  CONSTRAINT [FK_tbl_purchase_order_fulfulled_by_tbl_user_credentials] FOREIGN KEY([fulfilled_by])
REFERENCES [dbo].[tbl_user_credentials] ([id]);
ALTER TABLE [dbo].[tbl_purchase_order] ADD  CONSTRAINT [FK_tbl_purchase_order_operated_by_tbl_user_credentials] FOREIGN KEY([operated_by])
REFERENCES [dbo].[tbl_user_credentials] ([id]);
GO

CREATE TABLE [dbo].[tbl_transactions](
    [transaction_id] [varchar](100) NOT NULL,
    [transaction_date] [datetime] NULL,
    [operated_by] [int] NULL,
    [payment_received] [decimal](10, 2) NULL,
    PRIMARY KEY CLUSTERED 
    (
        [transaction_id] ASC
    )
);
GO

ALTER TABLE [dbo].[tbl_transactions] ADD  CONSTRAINT [DF__tbl_trans__trans__59C55456]  DEFAULT (getdate()) FOR [transaction_date];
ALTER TABLE [dbo].[tbl_transactions] ADD  CONSTRAINT [FK__tbl_trans__opera__607251E5] FOREIGN KEY([operated_by])
REFERENCES [dbo].[tbl_user_credentials] ([id]);
GO

CREATE TABLE [dbo].[tbl_purchase_order_details](
    [id] [int] IDENTITY(1,1) NOT NULL,
    [order_id] [int] NULL,
    [item_id] [int] NULL,
    [order_quantity] [int] NULL,
    [total] [decimal](18, 2) NULL,
    [received_quantity] [int] NULL,
    PRIMARY KEY CLUSTERED 
    (
        [id] ASC
    )
);
GO

ALTER TABLE [dbo].[tbl_purchase_order_details] ADD  CONSTRAINT [FK__tbl_purch__item___4E53A1AA] FOREIGN KEY([item_id])
REFERENCES [dbo].[tbl_item_master_data] ([id]);
ALTER TABLE [dbo].[tbl_purchase_order_details] ADD  CONSTRAINT [FK__tbl_purch__order__6AEFE058] FOREIGN KEY([order_id])
REFERENCES [dbo].[tbl_purchase_order] ([order_id]);
GO

CREATE TABLE [dbo].[tbl_supplier_items](
    [supplier_item_id] [int] IDENTITY(1,1) NOT NULL,
    [supplier_id] [int] NOT NULL,
    [item_id] [int] NOT NULL,
    PRIMARY KEY CLUSTERED 
    (
        [supplier_item_id] ASC
    )
);
GO

ALTER TABLE [dbo].[tbl_supplier_items] ADD  CONSTRAINT [FK__tbl_suppli__item___5165187F] FOREIGN KEY([item_id])
REFERENCES [dbo].[tbl_item_master_data] ([id]);
ALTER TABLE [dbo].[tbl_supplier_items] ADD  CONSTRAINT [FK__tbl_suppli__suppl__52593CB8] FOREIGN KEY([supplier_id])
REFERENCES [dbo].[tbl_suppliers] ([supplier_id]);
GO

CREATE TABLE [dbo].[tbl_transaction_details](
    [id] [int] IDENTITY(1,1) NOT NULL,
    [transaction_id] [varchar](100) NULL,
    [item_id] [int] NULL,
    [quantity] [int] NULL,
    [total] [decimal](18, 2) NULL,
    PRIMARY KEY CLUSTERED 
    (
        [id] ASC
    )
);
GO

ALTER TABLE [dbo].[tbl_transaction_details] ADD  CONSTRAINT [FK__tbl_trans__trans__5F7E2DAC] FOREIGN KEY([transaction_id])
REFERENCES [dbo].[tbl_transactions] ([transaction_id]);
ALTER TABLE [dbo].[tbl_transaction_details] ADD  CONSTRAINT [FK__tbl_trans__item___5E8A0973] FOREIGN KEY([item_id])
REFERENCES [dbo].[tbl_item_master_data] ([id]);
GO

CREATE TABLE [dbo].[tbl_inventory_report](
    [id] [int] IDENTITY(1,1) NOT NULL,
    [item_id] [int] NULL,
    [purchase_order_id] [int] NULL,
    [date] [datetime] NULL,
    [action][varchar](255)NULL,
    [quantity] [int] NULL,
    [modified_by] [int] NULL,
    [remarks][varchar](255)NULL,
    [old_stock] [int] NULL,
    [new_stock] [int] NULL,
    PRIMARY KEY CLUSTERED 
    (
        [id] ASC
    )
);
GO

ALTER TABLE [dbo].[tbl_inventory_report] ADD  CONSTRAINT [FK__tbl_invent__item___6A30C649] FOREIGN KEY([item_id])
REFERENCES [dbo].[tbl_item_master_data] ([id]);
ALTER TABLE [dbo].[tbl_inventory_report] ADD  CONSTRAINT [FK__tbl_invent__purch__693CA210] FOREIGN KEY([purchase_order_id])
REFERENCES [dbo].[tbl_purchase_order] ([order_id]);
ALTER TABLE [dbo].[tbl_inventory_report] ADD  CONSTRAINT [FK__tbl_invent__modif__6B24EA82] FOREIGN KEY([modified_by])
REFERENCES [dbo].[tbl_user_credentials] ([id]);
GO

CREATE TABLE [dbo].[tbl_audit_trail](
    [id] [int] IDENTITY(1,1) NOT NULL,
    [user_id] [int] NULL,
    [action][varchar](255)NULL,
    [date] [datetime] NULL,
    [details][varchar](255)NULL,
    PRIMARY KEY CLUSTERED 
    (
        [id] ASC
    )
);
GO

ALTER TABLE [dbo].[tbl_audit_trail] ADD  CONSTRAINT [FK__tbl_audit__user___6C190EBB] FOREIGN KEY([user_id])
REFERENCES [dbo].[tbl_user_credentials] ([id]);
GO

-- Insert default admin user
INSERT INTO [dbo].[tbl_user_credentials] ([first_name], [last_name], [username], [password], [designation]) VALUES ('Super', 'Admin', 'sa', 'password', 'Super Admin');
GO